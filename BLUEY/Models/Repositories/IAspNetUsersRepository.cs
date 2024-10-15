using BLUEY.Models.ViewModels;

namespace BLUEY.Models.Repositories
{
    public interface IAspNetUsersRepository
    {
        public List<Users> GetAll();
        public Users Get(string Id);
        public List<UserRoleViewModel> GetUserRoles();
        public Users Set(Users user);
        public bool Update(Users user);
        public bool Delet(Users user);
    }
}
