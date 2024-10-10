
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;
using System.Data;
using System.Security;

namespace BLUEY.Models.Repositories
{
    public class AspNetUsersRepository : IAspNetUsersRepository
    {

        private IDbConnection _connection;

        public AspNetUsersRepository()
        {
            _connection = new MySqlConnection(@"Server=localhost;Port=3306;Database=BlueDB;User=root;Password=r3n4t0321;");
        }

        public List<Users> GetAll()
        {
            return _connection.Query<Users>("select * from bluedb.aspnetusers;").ToList();
        }

        public Users Get(string id)
        {
            return _connection.QueryFirstOrDefault<Users>("select * from bluedb.aspnetusers where Id = @Id;", new {Id = id });
        }
        public Users Set(Users user)
        {
            string sql = @"INSERT INTO bluedb.aspnetusers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) 
            VALUES(@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount)";
            
            _connection.Query(sql, user);
            return user;
        }

        public bool Update(Users user)
        {
            var sql = "UPDATE bluedb.aspnetusers SET UserName=@UserName , NormalizedUserName=@NormalizedUserName, Email=@Email, NormalizedEmail=@NormalizedEmail, EmailConfirmed=@EmailConfirmed, PasswordHash=@PasswordHash, SecurityStamp=@SecurityStamp, ConcurrencyStamp=@ConcurrencyStamp, PhoneNumber=@PhoneNumber, PhoneNumberConfirmed=0, TwoFactorEnabled=0, LockoutEnd=NULL, LockoutEnabled=0, AccessFailedCount=0 WHERE Id= @Id;";
            _connection.Execute(sql, user);
            return false;
        }

        public bool Delet(Users user)
        {
            _connection.Execute("delete from bluedb.aspnetusers where Id = @Id", new {Id = user.Id });
            return false;
        }

    }
}
