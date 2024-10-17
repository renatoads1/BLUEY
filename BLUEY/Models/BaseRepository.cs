using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace BLUEY.Models
{
    public class BaseRepository
    {
        public IDbConnection _conMariaDb;
        protected readonly FbConnection _conFirebird;

        public BaseRepository(IConfiguration configuration)
        {
            // Obtenha a string de conexão do appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _conMariaDb = new MySqlConnection(connectionString);

            var firebirdConnectionString = configuration.GetConnectionString("FirebirdConnection");
            _conFirebird = new FbConnection(firebirdConnectionString);
        }

        // Métodos para abrir e fechar a conexão MySQL
        public void OpenMySqlConnection()
        {
            if (_conMariaDb.State == ConnectionState.Closed)
                _conMariaDb.Open();
        }

        public void CloseMySqlConnection()
        {
            if (_conMariaDb.State == ConnectionState.Open)
                _conMariaDb.Close();
        }

        // Método para abrir a conexão Firebird
        public void OpenFirebirdConnection()
        {
            if (_conFirebird.State == System.Data.ConnectionState.Closed)
                _conFirebird.Open();
        }

        // Método para fechar a conexão Firebird
        public void CloseFirebirdConnection()
        {
            if (_conFirebird.State == System.Data.ConnectionState.Open)
                _conFirebird.Close();
        }

    }
}
