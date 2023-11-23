using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using QA.Dtos;
using QA.Interfaces;
using QA.Models;
using System.Security.Claims;

namespace QA.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public IdentityUserService(UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager)
        {
            userManager = manager;
            _signInManager = signInManager;
        }
        public Task<IdentityResult> AssignRolesToUser(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Authenticate(string username, string password, ModelStateDictionary modelState)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            var users = await userManager.FindByNameAsync(username);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(username);
                //// user = await _userManager.FindByNameAsync(username);
                ///

                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await userManager.GetRolesAsync(user)
                };
            }
            if (users == null)
            {
                modelState.AddModelError("Username", "User name is not correct");
            }
            else
            {
                modelState.AddModelError("Password", "Password is not correct");
            }
            return null;

        }

        public Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {



                //await userManager.AddToRolesAsync(user, data.Roles);
               
                var userDTO = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,

                    //Roles = await userManager.GetRolesAsync(user)

                };

                return userDTO;
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                  error.Code.Contains("Password") ? nameof(data.Password) :
                  error.Code.Contains("Email") ? nameof(data.Email) :
                  error.Code.Contains("UserName") ? nameof(data.Username) :
                  "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;

        }

        public async Task<int> UserCount()
        {
            var userCount = await GetAllUsers();
            return userCount.Count;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
         


            var users = await userManager.Users.ToListAsync();

            var usersToReturn = users.Where(u =>
            !userManager.IsInRoleAsync(u, "Admin").Result)


            .Select(u => new UserDto
            {

                Id = u.Id,
                Username = u.UserName,

            }).ToList();


            return usersToReturn;



        }

    }
}
