using Xamarin.Forms;

namespace Badges
{
    public class EditItemViewModel : EditorPageViewModel
    {
        public override Command SaveCommand { get; }

        public override Command DeleteCommand { get; }

        public EditItemViewModel(Badge item)
        {
            Title = "Изменить";
            ComposedItem = item;

            SaveCommand = new Command(async () =>
            {
                if (this.IsItemValid)
                {
                    await App.DataStore.UpdateItemAsync(this.ComposedItem);
                    this.OnItemChanged();
                }
            }, ()=> IsItemValid);

            DeleteCommand = new Command(async () =>
            {
                await App.DataStore.DeleteItemAsync(this.ComposedItem);
                this.OnItemChanged();
            });
        }
    }
}
