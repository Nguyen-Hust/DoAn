using Microsoft.AspNetCore.Identity;

namespace WebAPI.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@xyz.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@xyz.com",
                    Email = "admin@xyz.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "123456@abC").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
