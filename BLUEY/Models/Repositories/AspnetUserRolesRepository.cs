
using Dapper;

namespace BLUEY.Models.Repositories
{
    public class AspnetUserRolesRepository : BaseRepository,IAspnetUserRolesRepository
    {
        public AspnetUserRolesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool Set(string userId, string roleId)
        {
            using (var connection = _conMariaDb) // Certifique-se de que _conMariaDb esteja configurado para abrir uma nova conexão se necessário
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var ur = new Userroles
                        {
                            UserId = userId,
                            RoleId = roleId
                        };

                        string sql = @"INSERT INTO bluedb.aspnetuserroles (UserId, RoleId) VALUES(@UserId, @RoleId);";

                        connection.Execute(sql, ur, transaction: transaction);

                        // Commit da transação se tudo ocorreu bem
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Rollback da transação em caso de erro
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

    }
}
