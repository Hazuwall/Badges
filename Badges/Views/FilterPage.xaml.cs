using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
	public partial class FilterPage : ContentPage
    {
		public FilterPage()
		{
            InitializeComponent();
            BindingContext = new FilterViewModel();
        }
    }
}
