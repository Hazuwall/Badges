using SQLite;

namespace Badges
{
    [Table("sections")]
    public class Section
    {
        public static Section Undefined => new Section() { Id = 0, Name = "Не определена", Color = "#000000" };

        [PrimaryKey, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("color")]
        public string Color { get; set; }

        [Ignore]
        public string ImagePath
        {
            get { return "Icon" + this.Id.ToString() + ".png"; }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}