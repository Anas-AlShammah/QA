using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QA.Dtos;
using System.Security.Claims;

namespace QA.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);
        public Task<UserDto> Authenticate(string username, string password, ModelStateDictionary modelState);

        public Task<UserDto> GetUser(ClaimsPrincipal principal);
        public Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task Logout();
        Task<IdentityResult> AssignRolesToUser(string userName);
        Task<int> UserCount();
        Task<List<UserDto>> GetAllUsers();
    }
}
