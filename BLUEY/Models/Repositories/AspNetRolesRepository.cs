using BLUEY.Models.Repositories;
using Dapper;

namespace BLUEY.Models
{
    public class AspNetRolesRepository : BaseRepository, IAspNetRolesRepository
    {
        public AspNetRolesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Roles> Get()
        {
            return _conMariaDb.Query<Roles>(@"select * from bluedb.aspnetroles;").ToList();
        }

        public Roles GetById(string id)
        {
            return _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = id });
        }

        public Roles GetByName(string name)
        {
            return _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Name = @Name;",new { Name = name });
        }
        public Roles Add(Roles role)
        {
            string sql = @"INSERT INTO bluedb.aspnetroles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES(@Id, @Name, @NormalizedName, @ConcurrencyStamp);";
            _conMariaDb.Query(sql, role);
            var rolenew = _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = role.Id });
            return rolenew;
        }

        public Roles Update(Roles role)
        {
            string sql = @"UPDATE bluedb.aspnetroles SET Name=@Name, NormalizedName=@NormalizedName, ConcurrencyStamp=@ConcurrencyStamp WHERE Id=@Id;";
            _conMariaDb.Query(sql, role);
            var roleUP = _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = role.Id });
            return roleUP;
        }
        public Roles Delete(int id)
        {
            string sql = @"DELETE FROM bluedb.aspnetroles WHERE Id=@Id;";
            _conMariaDb.Query(sql, new {Id = id });
            var roleUP = _conMariaDb.QueryFirstOrDefault<Roles>(@"select * from bluedb.aspnetroles where Id = @Id;", new { Id = id });
            return roleUP;
        }
    }
}
