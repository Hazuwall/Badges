using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Badges
{
    public class ItemsViewModel : PageViewModel
    {
        private BadgeFilter _filter = new BadgeFilter(DateTime.Now.Year, Section.All.Id);

        private bool _isUpdating = false;
        public bool IsUpdating
        {
            get { return _isUpdating; }
            set { SetProperty(ref _isUpdating, value); }
        }

        public ObservableCollection<BadgeViewModel> Items { get; } = new ObservableCollection<BadgeViewModel>();
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command EditItemCommand { get; }
        public INavigation Navigation { get; set; }

        public ItemsViewModel()
        {
            Title = "Badges";
            LoadItemsCommand = new Command(StartLoadItems);
            AddItemCommand = new Command(() => Navigation.PushAsync(new EditorPage(new AddItemViewModel())),
                ()=> App.DataStore.IsAvailable);
            EditItemCommand = new Command((item) =>
            {
                var vm = new EditItemViewModel(((BadgeViewModel)item).Badge);
                Navigation.PushAsync(new EditorPage(vm));
            });

            MessagingCenter.Subscribe<EditorPageViewModel>(this, "ItemChanged", (sender) =>
            {
                StartLoadItems();
            });

            MessagingCenter.Subscribe<FilterViewModel, BadgeFilter>(this, FilterViewModel.FilterChangedMessage, (sender, filter) =>
            {
                _filter = filter;
                StartLoadItems();
            });

            App.DataStore.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(App.DataStore.IsAvailable))
                {
                    StartLoadItems();
                    AddItemCommand?.ChangeCanExecute();
                }
            };
        }

        private async void StartLoadItems()
        {
            if (IsUpdating)
                return;
            IsUpdating = true;

            try
            {
                Items.Clear();
                var sectionMap = new Dictionary<int,Section>();
                foreach (var section in App.DataStore.GetGroups())
                    sectionMap.Add(section.Id, section);
                foreach (var item in await App.DataStore.GetItemsAsync(_filter, true))
                {
                    Items.Add(new BadgeViewModel()
                    {
                        Badge = item,
                        Section = sectionMap[item.SectionId]
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsUpdating = false;
            }
        }
    }
}
