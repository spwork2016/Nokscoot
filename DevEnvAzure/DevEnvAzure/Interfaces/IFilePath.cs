using SQLite.Net;
using System.Threading.Tasks;
namespace DevEnvAzure
{
    public interface IFilePath
    {
        SQLiteConnection GetConnection();
    }
}
