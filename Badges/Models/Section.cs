using SQLite;

namespace Badges
{
    [Table("sections")]
    public class Section
    {
        public static Section All { get; } = new Section() { Id = 0, Name = "Все", Color = "#000000" };

        [PrimaryKey, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("color")]
        public string Color { get; set; }

        [Ignore]
        public string ImagePath => string.Format("Icon{0}.png",Id);

        public override string ToString()
        {
            return this.Name;
        }
    }
}