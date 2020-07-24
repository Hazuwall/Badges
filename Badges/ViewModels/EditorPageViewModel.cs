using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Badges
{
    public abstract class EditorPageViewModel : PageViewModel
    {
        public Badge Item { get; set; }
        public abstract Task<bool> SaveAsync();
        public abstract Task<bool> DeleteAsync();
        public bool IsItemValid => !String.IsNullOrWhiteSpace(Item.Title) && Item.SectionId != 0;
    }
}