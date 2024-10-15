using Microsoft.AspNetCore.Identity;

namespace BLUEY.Models
{
    public class Roles : IdentityRole
    {
        public string Description { get; set; }
    }
}
