using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Badges
{
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            viewModel.Navigation = Navigation;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                viewModel.EditItemCommand.Execute(args.SelectedItem);

                // Manually deselect item
                ItemsListView.SelectedItem = null;
            }
        }

        protected override void OnAppearing()
        {
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
            base.OnAppearing();
        }
    }
}