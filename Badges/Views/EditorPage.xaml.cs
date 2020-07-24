using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Badges
{
    public partial class EditorPage : ContentPage
    {
        private readonly EditorPageViewModel _viewModel;

        public EditorPage(EditorPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
            viewModel.Navigation = Navigation;
        }
        public EditorPage():this(new AddItemViewModel())
        {
        }
    }
}
