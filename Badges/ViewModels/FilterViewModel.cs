using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Badges
{
    public class FilterViewModel : PageViewModel
    {
        public ObservableCollection<Section> Sections { get; set; }

        public FilterViewModel()
        {
            Title = "Фильтр";
            Sections = new ObservableCollection<Section>(DataStore.GetGroups());

            DataStore.PropertyChanged += (sender, e) =>
            {
                if(e.PropertyName == nameof(DataStore.IsAvailable) && DataStore.IsAvailable)
                {
                    Sections.Clear();
                    foreach (var item in DataStore.GetGroups())
                        Sections.Add(item);
                }
            };
        }
    }
}
