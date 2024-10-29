using Microsoft.AspNetCore.Identity;

namespace BLUEY.Models.Repositories
{
    public interface IAspNetRolesRepository
    {
        public List<IdentityRole> Get();
        public IdentityRole GetById(string id);
        public IdentityRole GetByName(string name);
        public IdentityRole Add(IdentityRole role);
        public IdentityRole Update(IdentityRole role);
        public void Delete(int id);

    }
}
