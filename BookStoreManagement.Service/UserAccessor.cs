using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookStoreManagement.Service
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public UserAccessor(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public string GetUserDisplayName(string userId)
        {
            return _userManager.FindByIdAsync(userId.ToString()).Result.DisplayName;
        }

        public string GetUserEmail()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
