using BLUEY.Data;
using BLUEY.Models.Repositories;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace BLUEY.Models
{
    public class AspNetRolesRepository : BaseRepository, IAspNetRolesRepository
    {
        private readonly ApplicationDbContext _contextM;
        public AspNetRolesRepository(ApplicationDbContext context, IConfiguration configuration) : base(configuration)
        {
            _contextM = context;
        }

        public List<IdentityRole> Get()
        {
            return _contextM.roles.ToList();
            //return _conMariaDb.Query<Roles>(@"select * from bluedb.aspnetroles;").ToList();
        }

        public IdentityRole GetById(string id)
        {
            return _contextM.roles.Where(a => a.Id == id).FirstOrDefault();
            //return _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = id });
        }

        public IdentityRole GetByName(string name)
        {
            return _contextM.roles.Where(a => a.Name == name).FirstOrDefault();
            //return _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Name = @Name;",new { Name = name });
        }
        public IdentityRole Add(IdentityRole role)
        {
            _contextM.roles.Add(role);
            _contextM.SaveChanges();
            return _contextM.roles.Where(a => a.Id == role.Id).FirstOrDefault();
            
            //string sql = @"INSERT INTO bluedb.aspnetroles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES(@Id, @Name, @NormalizedName, @ConcurrencyStamp);";
            //_conMariaDb.Query(sql, role);
            //var rolenew = _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = role.Id });
            //return rolenew;
        }

        public IdentityRole Update(IdentityRole role)
        {
            //_contextM.roles.Update(role);
            //_contextM.SaveChanges();
            return role;
            
            //string sql = @"UPDATE bluedb.aspnetroles SET Name=@Name, NormalizedName=@NormalizedName, ConcurrencyStamp=@ConcurrencyStamp WHERE Id=@Id;";
            //_conMariaDb.Query(sql, role);
            //var roleUP = _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = role.Id });
            //return roleUP;
        }
        public void Delete(int id)
        {
            _contextM.Remove(id);
            _contextM.Dispose();

            //string sql = @"DELETE FROM bluedb.aspnetroles WHERE Id=@Id;";
            //_conMariaDb.Query(sql, new {Id = id });
            //var roleUP = _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = id });
            
        }

    }
}
