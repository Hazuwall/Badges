using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Badges
{
    public class ItemsViewModel : PageViewModel
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ObservableCollection<Badge> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public BadgeFilter Filter { get; set; }

        public ItemsViewModel()
        {
            Title = "Badges";
            Items = new ObservableCollection<Badge>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Filter = new BadgeFilter(DateTime.Now.Year, Section.Undefined.Id);

            MessagingCenter.Subscribe<ItemEditorPage, Badge>(this, "ItemChanged", async (obj, item) =>
            {
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<FilterPage, int>(this, "YearChanged", async (obj, year) =>
            {
                Filter.Year = year;
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<FilterPage, int>(this, "SectionChanged", async (obj, id) =>
            {
                Filter.SectionId = (Filter.SectionId == id) ? Section.Undefined.Id : id;
                await ExecuteLoadItemsCommand();
            });

            DataStore.PropertyChanged += async (sender, e) =>
            {
                if (e.PropertyName == nameof(DataStore.IsAvailable) && DataStore.IsAvailable)
                    await ExecuteLoadItemsCommand();
            };
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Items.Clear();
                foreach (var item in await DataStore.GetItemsAsync(Filter, true))
                    Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
