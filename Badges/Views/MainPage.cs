using System;

using Xamarin.Forms;

namespace Badges
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage = new ItemsPage()
            {
                Title = "Badges"
            };

            Page filterPage = new FilterPage()
            {
                Title = "Фильтр"
            };

            Children.Add(itemsPage);
            Children.Add(filterPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
