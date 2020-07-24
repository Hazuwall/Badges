using System;
using Xamarin.Forms;

namespace Badges
{
    public partial class App : Application
    {
        public AppViewModel ViewModel { get; }

        public App(AppViewModel appViewModel)
        {
            ViewModel = appViewModel;
            
            InitializeComponent();
            
            if (Device.RuntimePlatform == Device.Android)
                MainPage = new NavigationPage(new MainPage());
            else
                throw new NotImplementedException();
        }
    }
}