using Microsoft.AspNetCore.Identity;

namespace BLUEY.Models
{
    public class Users : IdentityUser
    {

        public IdentityRole Roles { get; set; }
        public Userroles UserRoles { get; set; }
    }
}
