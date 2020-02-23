using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges
{
    public class BadgesDb : IDataStore<Badge, Section, BadgeFilter>
    {
        private SQLiteConnection _Database;

        public BadgesDb()
        {
            string databasePath = Droid.StorageHelper.GetDatabasePath();
            System.Diagnostics.Debug.WriteLine(databasePath);
            bool doExist = File.Exists(databasePath);

            SQLiteConnection database = new SQLiteConnection(databasePath);
            database.CreateTable<Badge>();
            if (!doExist)
            {
                database.CreateTable<Section>();
                database.CreateTable<Badge>();
                Section[] sections = new Section[16];
                sections[0] = new Section() { Id = 1, Name = "Музыкант", Color = "#2b1145" };
                sections[1] = new Section() { Id = 2, Name = "Кулинар", Color = "#ffb200" };
                sections[2] = new Section() { Id = 3, Name = "Учёный", Color = "#006633" };
                sections[3] = new Section() { Id = 4, Name = "Спортсмен", Color = "#de5d1c" };
                sections[4] = new Section() { Id = 5, Name = "Долгожитель", Color = "#0080ae" };
                sections[5] = new Section() { Id = 6, Name = "Путешественник", Color = "#005900" };
                sections[6] = new Section() { Id = 7, Name = "Хранитель очага", Color = "#336699" };
                sections[7] = new Section() { Id = 8, Name = "Геймер", Color = "#a10000" };
                sections[8] = new Section() { Id = 9, Name = "Режиссёр", Color = "#666666" };
                sections[9] = new Section() { Id = 10, Name = "Экстраверт", Color = "#66cc33" };
                sections[10] = new Section() { Id = 11, Name = "Программист", Color = "#660046" };
                sections[11] = new Section() { Id = 12, Name = "Писатель", Color = "#a01828" };
                sections[12] = new Section() { Id = 13, Name = "Киноман", Color = "#f7455b" };
                sections[13] = new Section() { Id = 14, Name = "Полиглот", Color = "#3c4065" };
                sections[14] = new Section() { Id = 15, Name = "Архивариус", Color = "#64798a" };
                sections[15] = new Section() { Id = 16, Name = "Художник", Color = "#01598a" };
                database.InsertAll(sections);
            }
            this._Database = database;
        }

        public Task<bool> AddItemAsync(Badge item)
        {
            return Task.FromResult(_Database.Insert(item) == 1);
        }

        public Task<bool> DeleteItemAsync(Badge item)
        {
            return Task.FromResult(_Database.Delete(item) == 1);
        }

        public Task<Badge> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Badge>> GetItemsAsync(BadgeFilter filter, bool forceRefresh)
        {
            DateTime startDate = new DateTime(filter.Year, 1, 1, 0, 0, 0);
            DateTime endDate = new DateTime(filter.Year + 1, 1, 1, 0, 0, 0);
            List<Badge> list = (from item in _Database.Table<Badge>() where (item.Date > startDate && item.Date < endDate && (filter.SectionId == 0 || item.SectionId == filter.SectionId)) orderby item.Date descending select item).ToList();
            foreach (Badge item in list)
                item.Section = _Database.Get<Section>(item.SectionId);
            return await Task.FromResult(list);
        }

        public Task<bool> UpdateItemAsync(Badge item)
        {
            return Task.FromResult(_Database.Update(item) == 1);
        }

        public IEnumerable<Section> GetGroups()
        {
            return (from item in _Database.Table<Section>() orderby item.Name select item).ToList();
        }
    }
}
