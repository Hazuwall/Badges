using System;

using Xamarin.Forms;

namespace Badges
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage = null;

            itemsPage = new ItemsPage()
            {
                Title = "Badges"
            };

            aboutPage = new FilterPage()
            {
                Title = "Фильтр"
            };

            Children.Add(itemsPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
