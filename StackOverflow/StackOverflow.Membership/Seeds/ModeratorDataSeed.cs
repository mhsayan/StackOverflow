using Autofac;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Membership.Entities;

namespace StackOverflow.Membership.Seeds
{
    public class ModeratorDataSeed
    {
        private UserManager<ApplicationUser> _userManager;

        public ModeratorDataSeed(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ModeratorDataSeed()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        public async Task SeedUserAsync()
        {
            var moderatorUser = new ApplicationUser
            {
                FirstName = "David",
                LastName = "Warner",
                UserName = "moderator@stackoverflow.com",
                Email = "moderator@stackoverflow.com",
                EmailConfirmed = true
            };

            const string password = "moderator@stackoverflow";
            var user = await _userManager.FindByEmailAsync(moderatorUser.Email);

            if (user == null)
            {
                var result = await _userManager.CreateAsync(moderatorUser, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(moderatorUser, "Moderator");
                }
            }
        }
    }
}
