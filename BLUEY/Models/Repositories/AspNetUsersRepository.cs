using BLUEY.Models.ViewModels;
using Dapper;
using MySqlX.XDevAPI.Common;
using System.Data;

namespace BLUEY.Models.Repositories
{
    public class AspNetUsersRepository : BaseRepository, IAspNetUsersRepository
    {
        public AspNetUsersRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Users> GetAll()
        {
            return _conMariaDb.Query<Users>("select * from bluedb.aspnetusers;").ToList();
        }

        public Users Get(string id)
        {
            return _conMariaDb.QueryFirstOrDefault<Users>("select * from bluedb.aspnetusers where Id = @Id;", new {Id = id });
        }

        public List<UserRoleViewModel> GetUserRoles()
        {
            string sql = @"SELECT *
               FROM BlueDB.AspNetUsers AS u
               LEFT JOIN BlueDB.AspNetUserRoles AS a ON a.UserId = u.Id
               LEFT JOIN BlueDB.AspNetRoles AS r ON r.Id = a.RoleId;";

            return _conMariaDb.Query<Users, Userroles, Roles, UserRoleViewModel>(
                sql,
                (user, userroles,role) =>
                {
                    return new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                },
                splitOn: "UserId,RoleId" // Indica ao Dapper onde começar a mapear a entidade `Roles`
            ).ToList();



        }


        public Users Set(Users user)
        {
            string sql = @"INSERT INTO bluedb.aspnetusers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) 
            VALUES(@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount)";

            _conMariaDb.Query(sql, user);
            return user;
        }

        public bool Update(Users user)
        {
            var sql = "UPDATE bluedb.aspnetusers SET UserName=@UserName , NormalizedUserName=@NormalizedUserName, Email=@Email, NormalizedEmail=@NormalizedEmail, EmailConfirmed=@EmailConfirmed, PasswordHash=@PasswordHash, SecurityStamp=@SecurityStamp, ConcurrencyStamp=@ConcurrencyStamp, PhoneNumber=@PhoneNumber, PhoneNumberConfirmed=0, TwoFactorEnabled=0, LockoutEnd=NULL, LockoutEnabled=0, AccessFailedCount=0 WHERE Id= @Id;";
            _conMariaDb.Execute(sql, user);
            return false;
        }

        public bool Delet(Users user)
        {
            _conMariaDb.Execute("delete from bluedb.aspnetusers where Id = @Id", new {Id = user.Id });
            return false;
        }

    }
}
