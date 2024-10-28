
using BLUEY.Data;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLUEY.Models.Repositories
{
    public class AspnetUserRolesRepository : BaseRepository,IAspnetUserRolesRepository
    {
        //private readonly ApplicationDbContext _contextM;
        public AspnetUserRolesRepository(/*ApplicationDbContext context,*/ IConfiguration configuration) : base(configuration)
        {
            //_contextM = context;
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
