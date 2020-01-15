//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DevEnvAzure.Droid;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using System.IO;
//using Android.App;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Xamarin.Forms;

using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

[assembly: Dependency(typeof(DevEnvAzure.Droid.SqlLiteService))]
namespace DevEnvAzure.Droid
{
    class SqlLiteService : IDataService
    {
        public SqlLiteService() { }

        #region ISQLite implementation
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            try
            {
                var sqliteFilename = "NokScoot.db3";
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, sqliteFilename);
                if (!File.Exists(path))
                    File.Create(path);
                // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                // return Path.Combine(path, filename);
                //var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
                var conn = new SQLite.Net.SQLiteConnection(platform, path);
                //  var conn = new SQLite.SQLiteConnection(path);
                return conn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


    }
}