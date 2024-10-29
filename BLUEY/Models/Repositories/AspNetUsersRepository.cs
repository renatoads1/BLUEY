using BLUEY.Data;
using BLUEY.Models.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Data;

namespace BLUEY.Models.Repositories
{
    public class AspNetUsersRepository : BaseRepository, IAspNetUsersRepository
    {
        private readonly ApplicationDbContext _contextM;
        public AspNetUsersRepository(ApplicationDbContext contextM, IConfiguration configuration) : base(configuration)
        {
            _contextM = contextM;
        }

        public List<Users> GetAll()
        {
            var users = _contextM.Users
            .Select(identityUser => new Users
            {
                Id = identityUser.Id,
                UserName = identityUser.UserName,
                Email = identityUser.Email,
                // Mapear outras propriedades específicas de `IdentityUser` se necessário.
                // Propriedades adicionais da classe `Users` também podem ser definidas aqui.
            })
            .ToList();

            return users;

            //return _conMariaDb.Query<Users>("select * from bluedb.aspnetusers;").ToList();

        }

        public Users Get(string id)
        {
            //não existe view
            var t = new IdentityUser();
            _contextM.
                Users.
                Select(a => new Users {
                Id = a.Id,
                UserName = a.UserName,
                Email = a.Email
                }).First();
            //return _conMariaDb.QueryFirstOrDefault<Users>("select * from bluedb.aspnetusers where Id = @Id;", new {Id = id });
            return new Users { Id = id };
        }

        public List<UserRoleViewModel> GetUserRoles()
        {
            var userRoles =  _contextM.Users
            .GroupJoin(
                _contextM.UserRoles,
                user => user.Id,
                userRole => userRole.UserId,
                (user, userRoles) => new { User = user, UserRoles = userRoles }
            )
            .SelectMany(
                x => x.UserRoles.DefaultIfEmpty(),
                (x, userRole) => new { x.User, UserRole = userRole }
            )
            .GroupJoin(
                _contextM.Roles,
                x => x.UserRole.RoleId,
                role => role.Id,
                (x, roles) => new { x.User, x.UserRole, Roles = roles }
            )
            .SelectMany(
                x => x.Roles.DefaultIfEmpty(),
                (x, role) => new UserRoleViewModel
                {
                    UserId = x.User.Id,
                    UserName = x.User.UserName,
                    Email = x.User.Email,
                    RoleId = role != null ? role.Id : null,
                    RoleName = role != null ? role.Name : null
                }
            )
            .ToList();

            return userRoles;
            //string sql = @"SELECT *
            //   FROM BlueDB.AspNetUsers AS u
            //   LEFT JOIN BlueDB.AspNetUserRoles AS a ON a.UserId = u.Id
            //   LEFT JOIN BlueDB.AspNetRoles AS r ON r.Id = a.RoleId;";

            //return _conMariaDb.Query<Users, Userroles, Roles, UserRoleViewModel>(
            //    sql,
            //    (user, userroles,role) =>
            //    {
            //        return new UserRoleViewModel
            //        {
            //            UserId = user.Id,
            //            UserName = user.UserName,
            //            Email = user.Email,
            //            RoleId = role.Id,
            //            RoleName = role.Name
            //        };
            //    },
            //    splitOn: "UserId,RoleId" // Indica ao Dapper onde começar a mapear a entidade `Roles`
            //).ToList();



        }


        public Users Set(Users user)
        {
            
            _contextM.Users.Add( user );
            _contextM.SaveChanges();
            return user;
            //string sql = @"INSERT INTO bluedb.aspnetusers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) 
            //VALUES(@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount)";

            //_conMariaDb.Query(sql, user);
            //return user;
        }

        public bool Update(Users user)
        {
            return false;
            //var sql = "UPDATE bluedb.aspnetusers SET UserName=@UserName , NormalizedUserName=@NormalizedUserName, Email=@Email, NormalizedEmail=@NormalizedEmail, EmailConfirmed=@EmailConfirmed, PasswordHash=@PasswordHash, SecurityStamp=@SecurityStamp, ConcurrencyStamp=@ConcurrencyStamp, PhoneNumber=@PhoneNumber, PhoneNumberConfirmed=0, TwoFactorEnabled=0, LockoutEnd=NULL, LockoutEnabled=0, AccessFailedCount=0 WHERE Id= @Id;";
            //_conMariaDb.Execute(sql, user);
            //return false;
        }

        public bool Delet(Users user)
        {
            return false;
            //_conMariaDb.Execute("delete from bluedb.aspnetusers where Id = @Id", new {Id = user.Id });
            //return false;
        }

    }
}
