using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Badges
{
    public partial class ItemEditorPage : ContentPage
    {
        private BaseEditorViewModel viewModel;
        private ObservableCollection<Section> sections;

        public ItemEditorPage(BaseEditorViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            SectionsListView.ItemsSource = sections = new ObservableCollection<Section>(viewModel.DataStore.GetGroups());
            Section currentSection = sections.FirstOrDefault(t => t.Id == viewModel.Item.SectionId);
            SectionsListView.SelectedItem = currentSection;
        }
        public ItemEditorPage():this(new AddItemViewModel())
        {

        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (await viewModel.SaveAsync())
            {
                MessagingCenter.Send(this, "ItemChanged", viewModel.Item);
                await Navigation.PopToRootAsync();
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (await viewModel.DeleteAsync())
            {
                MessagingCenter.Send(this, "ItemChanged", viewModel.Item);
                await Navigation.PopToRootAsync();
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Section;
            if (item == null || viewModel.Item.Section.Id == item.Id)
                return;
            viewModel.Item.Section = item;
            viewModel.Item.SectionId = item.Id;
        }
    }
}
