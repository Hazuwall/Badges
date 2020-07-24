using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
    public class AddItemViewModel : EditorPageViewModel
    {
        public override Command SaveCommand { get; }

        public override Command DeleteCommand {get;}

        public AddItemViewModel()
        {
            Title = "Добавить";
            SaveCommand = new Command(async () =>
            {
                if (this.IsItemValid)
                {
                    await App.DataStore.AddItemAsync(this.ComposedItem);
                    this.OnItemChanged();
                }
            }, () => IsItemValid);
            DeleteCommand = new Command(async () =>
            {
                await Navigation.PopToRootAsync();
            });
        }
    }
}
