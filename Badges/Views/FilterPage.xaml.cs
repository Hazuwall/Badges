using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
	public partial class FilterPage : ContentPage
	{
        public ObservableCollection<Section> Sections { get; set; }
        public FilterViewModel ViewModel { get; set; }

		public FilterPage()
		{
            ViewModel = new FilterViewModel();
            Sections = new ObservableCollection<Section>(ViewModel.DataStore.GetGroups());
            InitializeComponent();
            BindingContext = this;

            int[] years = new int[5];
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
                years[i] = currentYear - i;
            YearPicker.ItemsSource = years;
		}

        private void YearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.YearPicker.SelectedIndex;
            if(index != -1)
                MessagingCenter.Send(this, "YearChanged", (int)this.YearPicker.SelectedItem);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Section;
            if (item == null)
                return;
            // Manually deselect item
            (sender as ListView).SelectedItem = null;

            MessagingCenter.Send(this, "SectionChanged", item.Id);
        }
    }
}
