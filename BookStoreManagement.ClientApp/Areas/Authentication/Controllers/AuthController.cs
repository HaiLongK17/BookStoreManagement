using AspNetCoreHero.ToastNotification.Abstractions;
using BookStoreManagement.ClientApp.Models;
using BookStoreManagement.ClientApp.Models.DTO;
using BookStoreManagement.ClientApp.Services.CookieService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly ICookieService _cookieService;
        public AuthController(IHttpClientFactory clientFactory, INotyfService notyf, ICookieService cookieService)
        {
            _clientFactory = clientFactory;
            _notyf = notyf;
            _client = _clientFactory.CreateClient("BookStoreClient");
            _cookieService = cookieService;
        }

        [Route("login", Name = "login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login", Name = "login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var loginRequest = await _client.PostAsync("/api/authentications/login", loginContent);

            if((int)loginRequest.StatusCode == 500)
            {
                _notyf.Error("Opps! Something went wrong");
                return View();
            }

            string loginJsonData = await loginRequest.Content.ReadAsStringAsync();
            Response<CookieDto> loginResponse = JsonConvert.DeserializeObject<Response<CookieDto>>(loginJsonData);

            if (loginResponse.StatusCode != 200)
            {
                _notyf.Error(string.Join(", ", loginResponse.Errors));
                return View();
            }

            _cookieService.SetCookie(loginResponse.Data);
            //if()
            return RedirectToAction("Index", "Account", new { area = "Admin" });
        }

        [Route("register", Name = "register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register", Name = "register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var registerContent = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");
            var registerRequest = await _client.PostAsync("/api/authentications/register", registerContent);

            if ((int)registerRequest.StatusCode == 500)
            {
                _notyf.Error("Opps! Something went wrong");
                return View();
            }

            string registerJsonData = await registerRequest.Content.ReadAsStringAsync();
            Response<CookieDto> registerResponse = JsonConvert.DeserializeObject<Response<CookieDto>>(registerJsonData);
            if (registerResponse.StatusCode != 200)
            {
                _notyf.Error(string.Join(", ", registerResponse.Errors));
                return View();
            }

            _cookieService.SetCookie(registerResponse.Data);

            return RedirectToAction("Login", "Auth", new { area = "Authentication" });
        }

        [HttpPost]
        [Route("logout", Name = "logout")]
        public IActionResult Logout()
        {
            _cookieService.ClearCookie();

            return RedirectToAction("Login", "Auth", new { area = "Authentication" });
        }
    }
}
