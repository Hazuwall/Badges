using System;
using System.Collections.Generic;
using System.Text;

namespace Badges
{
    public class BadgeFilter
    {
        public int Year { get; set; }
        public int SectionId { get; set; }

        public BadgeFilter(int Year, int SectionId)
        {
            this.Year = Year;
            this.SectionId = SectionId;
        }
    }
}
