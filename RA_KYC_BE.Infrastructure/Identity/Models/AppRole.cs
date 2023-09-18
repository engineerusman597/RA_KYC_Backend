using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace RA_KYC_BE.Infrastructure.Identity.Models
{
    public partial class AppRole : IdentityRole<int>
    {
        public AppRole() : base()
        {
        }

        public AppRole(string roleName)
        {
            Name = roleName;
        }
    }
}
