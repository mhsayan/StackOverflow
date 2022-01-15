using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StackOverflow.Membership.Entities;
using StackOverflow.Platform.Exceptions;

namespace StackOverflow.Platform.Services
{
    public class ProfileService : IProfileService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private static IHttpContextAccessor _httpContextAccessor;

        public ProfileService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetUserAsync(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
                throw new InvalidParameterException("User name must be provided to get a user.");

            return await _userManager.FindByEmailAsync(userName);
        }

        public async Task<ApplicationUser> GetUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new InvalidParameterException("User id must be provided to get a user.");

            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<ApplicationUser?> GetUserAsync()
        {
            var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;

            if (userName is null)
                return null;

            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            if (user == null)
                throw new InvalidParameterException("User must be provided to get user claims.");

            var oldUser = await GetUserAsync(user.UserName);

            if (oldUser == null)
                throw new InvalidParameterException("User must be provided to get user claims.");

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new InvalidParameterException("No user found with the user id.");

            return await _userManager.FindByIdAsync(userId.ToString());
        }
    }
}
