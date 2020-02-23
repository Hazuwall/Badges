using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Badges
{
    [Table("badges")]
    public class Badge
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("section_id")]
        public int SectionId { get; set; }

        [Ignore]
        public Section Section { get; set; }

        [Column("project_title")]
        public string ProjectTitle { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Ignore]
        public string DateString { get { return this.Date.ToString("yyyy/MM/dd"); } }
    }
}
