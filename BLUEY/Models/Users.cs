using Microsoft.AspNetCore.Identity;

namespace BLUEY.Models
{
    public class Users : IdentityUser
    {

        public Roles Roles { get; set; }
        public Userroles UserRoles { get; set; }
    }
}
