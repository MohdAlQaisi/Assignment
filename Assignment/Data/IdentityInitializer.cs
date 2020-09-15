using Microsoft.AspNetCore.Identity;

namespace Assignment.Data
{
    public static class IdentityInitializer
    {
        public static void Create(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            CreateRoles(roleManager);
            CreateUsers(userManager);
        }

        public static void CreateUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin@onesignal.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@onesignal.com";
                user.Email = "admin@onesignal.com";
                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Admin").Wait();
            }

            if (userManager.FindByNameAsync("operation@onesignal.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "operation@onesignal.com";
                user.Email = "operation@onesignal.com";
                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Operations").Wait();
                }
            }
        }

        public static void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Operations").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Operations";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
