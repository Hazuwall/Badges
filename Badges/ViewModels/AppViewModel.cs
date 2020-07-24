using System;
using System.Collections.Generic;
using System.Text;

namespace Badges
{
    public class AppViewModel
    {
        public IDataStore<Badge, Section, BadgeFilter> DataStore { get; }

        public AppViewModel(IDataStore<Badge, Section, BadgeFilter> dataStore)
        {
            DataStore = dataStore;
        }
    }
}
