using System;
using System.Threading.Tasks;

namespace Badges
{
    public class EditItemViewModel : BaseEditorViewModel
    {
        public EditItemViewModel(Badge item)
        {
            Title = "Изменить";
            Item = item;
        }

        public override async Task<bool> SaveAsync()
        {
            return this.IsItemValid && await DataStore.UpdateItemAsync(Item);
        }

        public override async Task<bool> DeleteAsync()
        {
            return await DataStore.DeleteItemAsync(Item);
        }
    }
}
