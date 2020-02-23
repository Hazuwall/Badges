using System;
using System.Threading.Tasks;

namespace Badges
{
    public class AddItemViewModel : BaseEditorViewModel
    {
        public AddItemViewModel()
        {
            Title = "Добавить";
            Item = new Badge()
            {
                Title = String.Empty,
                Date = DateTime.Now,
                ProjectTitle = String.Empty,
                Section = Section.Undefined,
                SectionId = 0
            };
        }

        public override async Task<bool> SaveAsync()
        {
            return this.IsItemValid && await DataStore.AddItemAsync(Item);
        }

        public override Task<bool> DeleteAsync()
        {
            return Task.FromResult(true);
        }
    }
}
