using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
    public class FilterViewModel : PageViewModel
    {
        public const string FilterChangedMessage = "FilterChanged";

        public ObservableCollection<Section> Sections { get; }
        public List<int> Years { get; }

        private Section _selectedSection = Section.All;
        public Section SelectedSection
        {
            get { return _selectedSection; }
            set { SetProperty(ref _selectedSection, value); }
        }

        public int _selectedYearIndex = 0;
        public int SelectedYearIndex
        {
            get { return _selectedYearIndex; }
            set { SetProperty(ref _selectedYearIndex, value); }
        }

        public FilterViewModel()
        {
            Title = "Фильтр";
            Sections = new ObservableCollection<Section>(App.DataStore.GetGroups());
            Sections.Insert(0, Section.All);

            Years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
                Years.Add(currentYear - i);

            App.DataStore.PropertyChanged += (sender, e) =>
            {
                if(e.PropertyName == nameof(App.DataStore.IsAvailable) && App.DataStore.IsAvailable)
                {
                    Sections.Clear();
                    Sections.Add(Section.All);
                    foreach (var item in App.DataStore.GetGroups())
                        Sections.Add(item);
                }
            };

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedSection) || e.PropertyName == nameof(SelectedYearIndex))
                {
                    int year = Years[SelectedYearIndex >= 0 ? SelectedYearIndex : 0];
                    int sectionId = (SelectedSection ?? Section.All).Id;
                    var filter = new BadgeFilter(year, sectionId);
                    MessagingCenter.Send(this, FilterChangedMessage, filter);
                }
            };
        }
    }
}
