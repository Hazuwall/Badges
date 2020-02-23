using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Badges.Droid
{
    public static class StorageHelper
    {
        public static string GetDatabasePath()
        {
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Badges", "Badges.db3");
        }
    }
}