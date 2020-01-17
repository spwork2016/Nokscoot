using SQLite;

namespace DevEnvAzure
{
    public interface IDataService
    {
        SQLiteConnection GetConnection();
    }
}
