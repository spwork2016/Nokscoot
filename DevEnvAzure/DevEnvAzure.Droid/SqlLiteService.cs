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
        public SQLiteConnection GetConnection()
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
                //var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                SQLiteConnection conn = new SQLiteConnection(path);
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