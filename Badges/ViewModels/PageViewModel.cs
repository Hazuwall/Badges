using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
    public class PageViewModel : NotifyPropertyChanged
    {
        public AppViewModel App { get; }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public PageViewModel()
        {
            App = (Application.Current as App).ViewModel;
        }
    }
}
