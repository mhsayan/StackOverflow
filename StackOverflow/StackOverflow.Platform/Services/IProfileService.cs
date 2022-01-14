using System.Security.Claims;
using StackOverflow.Membership.Entities;

namespace StackOverflow.Platform.Services
{
    public interface IProfileService
    {
        Task<ApplicationUser> GetUserAsync(string userName);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserAsync(Guid userId);
        Task<ApplicationUser> GetUserAsync();
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    }
}
