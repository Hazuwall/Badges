using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
    public abstract class EditorPageViewModel : PageViewModel
    {
        public const string ItemChangedMessage = "ItemChanged";

        public List<Section> Sections { get; }
        public int Id { get; private set; }

        private string _itemTitle = string.Empty;
        public string ItemTitle
        {
            get { return _itemTitle; }
            set {
                SetProperty(ref _itemTitle, value);
                OnPropertyChanged(nameof(IsItemValid));
                SaveCommand?.ChangeCanExecute();
            }
        }

        private string _projectTitle = string.Empty;
        public string ProjectTitle
        {
            get { return _projectTitle; }
            set { SetProperty(ref _projectTitle, value); }
        }

        private DateTime _date = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private Section _selectedSection = null;
        public Section SelectedSection
        {
            get { return _selectedSection; }
            set {
                SetProperty(ref _selectedSection, value);
                OnPropertyChanged(nameof(IsItemValid));
                SaveCommand?.ChangeCanExecute();
            }
        }

        public bool IsItemValid => !string.IsNullOrWhiteSpace(ItemTitle) && SelectedSection != null;

        public Badge ComposedItem
        {
            get
            {
                return new Badge()
                {
                    Id = Id,
                    Title = ItemTitle,
                    ProjectTitle = ProjectTitle,
                    Date = SelectedDate,
                    SectionId = SelectedSection.Id
                };
            }
            set
            {
                Id = value.Id;
                ItemTitle = value.Title;
                ProjectTitle = value.ProjectTitle;
                SelectedDate = value.Date;
                SelectedSection = Sections.FirstOrDefault(t => t.Id == value.SectionId);
            }
        }

        public abstract Command SaveCommand { get; }
        public abstract Command DeleteCommand { get; }
        public INavigation Navigation { get; set; }

        public EditorPageViewModel()
        {
            Sections = App.DataStore.GetGroups().ToList();
        }

        protected async void OnItemChanged()
        {
            MessagingCenter.Send(this, ItemChangedMessage);
            await Navigation.PopToRootAsync();
        }
    }
}