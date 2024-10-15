using MySql.Data.MySqlClient;
using System.Data;

namespace BLUEY.Models
{
    public class BaseRepository
    {
        public IDbConnection _conMariaDb;

        public BaseRepository(IConfiguration configuration)
        {
            // Obtenha a string de conexão do appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _conMariaDb = new MySqlConnection(connectionString);
        }
    }
}
