using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Badges
{
    public class BadgesDb : IDataStore<Badge, Section, BadgeFilter>
    {
        private readonly string _path;
        private SQLiteConnection _database = null;

        private bool _isAvailable;
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { SetProperty(ref _isAvailable, value); }
        }

        public BadgesDb(string path)
        {
            _path = path;
        }

        public void Initialize()
        {
            try
            {
                bool doExist = File.Exists(_path);
                SQLiteConnection database = new SQLiteConnection(_path);
                database.CreateTable<Badge>();
                if (!doExist)
                {
                    database.CreateTable<Section>();
                    database.CreateTable<Badge>();
                    Section[] sections = new Section[16];
                    sections[0] = new Section() { Id = 1, Name = "Музыка", Color = "#2b1145" };
                    sections[1] = new Section() { Id = 2, Name = "Кулинария", Color = "#ffb200" };
                    sections[2] = new Section() { Id = 3, Name = "Учеба", Color = "#006633" };
                    sections[3] = new Section() { Id = 4, Name = "Спорт", Color = "#de5d1c" };
                    sections[4] = new Section() { Id = 5, Name = "Здоровье", Color = "#0080ae" };
                    sections[5] = new Section() { Id = 6, Name = "Путешествия", Color = "#005900" };
                    sections[6] = new Section() { Id = 7, Name = "Дом", Color = "#336699" };
                    sections[7] = new Section() { Id = 8, Name = "Игры", Color = "#a10000" };
                    sections[8] = new Section() { Id = 9, Name = "Видео", Color = "#666666" };
                    sections[9] = new Section() { Id = 10, Name = "Люди", Color = "#66cc33" };
                    sections[10] = new Section() { Id = 11, Name = "Программирование", Color = "#660046" };
                    sections[11] = new Section() { Id = 12, Name = "Литература", Color = "#a01828" };
                    sections[12] = new Section() { Id = 13, Name = "Кино", Color = "#f7455b" };
                    sections[13] = new Section() { Id = 14, Name = "Языки", Color = "#3c4065" };
                    sections[14] = new Section() { Id = 15, Name = "Порядок", Color = "#64798a" };
                    sections[15] = new Section() { Id = 16, Name = "Искусство", Color = "#01598a" };
                    database.InsertAll(sections);
                }
                _database = database;
                IsAvailable = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public Task<bool> AddItemAsync(Badge item)
        {
            if (IsAvailable)
                return Task.FromResult(_database.Insert(item) == 1);
            else
                return Task.FromResult(false);
        }

        public Task<bool> DeleteItemAsync(Badge item)
        {
            if(IsAvailable)
                return Task.FromResult(_database.Delete(item) == 1);
            else
                return Task.FromResult(false);
        }

        public Task<Badge> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Badge>> GetItemsAsync(BadgeFilter filter, bool forceRefresh)
        {
            if (!IsAvailable)
                return Task.FromResult(Enumerable.Empty<Badge>());

            DateTime startDate = new DateTime(filter.Year, 1, 1, 0, 0, 0);
            DateTime endDate = new DateTime(filter.Year + 1, 1, 1, 0, 0, 0);
            var items = (from item in _database.Table<Badge>() where item.Date >= startDate && item.Date < endDate && (filter.SectionId == 0 || item.SectionId == filter.SectionId) orderby item.Date descending select item).ToList();
            foreach (Badge item in items)
                item.Section = _database.Get<Section>(item.SectionId);
            return Task.FromResult<IEnumerable<Badge>>(items);
        }

        public Task<bool> UpdateItemAsync(Badge item)
        {
            if (IsAvailable)
                return Task.FromResult(_database.Update(item) == 1);
            else
                return Task.FromResult(false);
        }

        public IEnumerable<Section> GetGroups()
        {
            if (IsAvailable)
                return (from item in _database.Table<Section>() orderby item.Name select item).ToList();
            else
                return Enumerable.Empty<Section>();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
