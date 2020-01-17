using System;
using Xamarin.Forms;
using System.IO;
using SQLite;

[assembly: Dependency(typeof(DevEnvAzure.iOS.SqliteService))]
namespace DevEnvAzure.iOS
{
    public class SqliteService : IDataService
    {
        public SqliteService()
        {
        }
        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "NokScoot.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            var conn = new SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
        #endregion
    }
}