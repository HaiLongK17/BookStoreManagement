using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    public class AuthenticationsController : BaseApiController
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationsController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return HandleResult(await _authService.Login(loginDto));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            return HandleResult(await _authService.Register(registerDto));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] CookieDto cookie)
        {
            return HandleResult(await _authService.RefreshToken(cookie));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetCurrentUser([FromBody] CookieDto cookie)
        {
            return HandleResult(await _authService.GetCurrentUser(cookie));
        }
    }
}
