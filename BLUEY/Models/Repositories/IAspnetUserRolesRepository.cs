namespace BLUEY.Models.Repositories
{
    public interface IAspnetUserRolesRepository
    {
        public bool Set(string UserId, string RoleId);
    }
}
