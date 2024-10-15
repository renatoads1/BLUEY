namespace BLUEY.Models.Repositories
{
    public interface IAspNetRolesRepository
    {
        public List<Roles> Get();
        public Roles GetById(string id);
        public Roles GetByName(string name);
        public Roles Add(Roles role);
        public Roles Update(Roles role);
        public Roles Delete(int id);

    }
}
