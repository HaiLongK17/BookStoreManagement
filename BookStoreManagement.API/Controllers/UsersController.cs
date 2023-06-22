using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("role/{role}")]
        public async Task<IActionResult> GetUsersInRole(string role)
        {
            return HandleResult(await _userService.GetUsersInRole(role));
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return HandleResult(await _userService.GetUserById(userId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            return HandleResult(await _userService.CreateUser(userDto));
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UpdateUserDto userDto)
        {
            return HandleResult(await _userService.UpdateUser(id, userDto));
        }

        [Authorize]
        [HttpPost]
        [Route("{userId}/password")]
        public async Task<IActionResult> ChangeUserPassword(Guid userId, [FromBody] ChangePasswordDto passwordDto)
        {
            return HandleResult(await _userService.ChangePassword(userId, passwordDto));
        }

        [Authorize]
        [HttpPost]
        [Route("{userId}/lock")]
        public async Task<IActionResult> LockUser(Guid userId)
        {
            return HandleResult(await _userService.LockUser(userId));
        }

        [Authorize]
        [HttpPost]
        [Route("{userId}/unlock")]
        public async Task<IActionResult> UnLockUser(Guid userId)
        {
            return HandleResult(await _userService.UnlockUser(userId));
        }
    }
}
