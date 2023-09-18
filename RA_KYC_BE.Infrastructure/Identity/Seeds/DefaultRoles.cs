using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using RA_KYC_BE.Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRoles(RoleManager<AppRole> roleManager)
        {
            await roleManager.CreateAsync(new AppRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new AppRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new AppRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new AppRole(Roles.User.ToString()));

        }

    }
}
