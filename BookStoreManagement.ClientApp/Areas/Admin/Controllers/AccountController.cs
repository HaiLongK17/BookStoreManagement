using AspNetCoreHero.ToastNotification.Abstractions;
using BookStoreManagement.ClientApp.Models.ViewModels;
using BookStoreManagement.ClientApp.Services.UserService;
using BookStoreManagement.ClientApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using BookStoreManagement.ClientApp.Services.ModelService;
using BookStoreManagement.ClientApp.Helpers;
using BookStoreManagement.ClientApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace BookStoreManagement.ClientApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly string _jwt;
        public AccountController(IHttpClientFactory clientFactory, INotyfService notyf,
            IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _clientFactory = clientFactory;
            _notyf = notyf;
            _client = _clientFactory.CreateClient("BookStoreClient");
            _userService = userService;
            _jwt = _contextAccessor.HttpContext?.Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userService.GetUser();

            if (currentUser == null) return UnAuthorized();

            var userVM = new AccountViewModel
            {
                User = currentUser,
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto passwordDto)
        {
            var currentUser = await _userService.GetUser();

            var userContent = new StringContent(JsonConvert.SerializeObject(passwordDto), Encoding.UTF8, "application/json");
            var updateRequest = await _client.PostAsync($"/api/users/{currentUser.Id}/password", userContent);
            if ((int)updateRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                updateRequest = await _client.PostAsync($"/api/users/{currentUser.Id}/password", userContent);
            }

            string userJsonData = await updateRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(userJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    html = RenderHelper.RenderRazorViewToString(this, "_PasswordForm", passwordDto)
                });
            }

            return Json(new
            {
                isValid = true,
                message = "Change password successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_PasswordForm", new ChangePasswordDto())
            });
        }

        private async Task<bool> RefreshToken()
        {
            var newCookie = await _userService.RefreshToken();

            if (newCookie == null) return false;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newCookie.Token);

            return true;
        }

        private IActionResult UnAuthorized()
        {
            _notyf.Error("Unauthorized");
            return RedirectToAction("Index", "Home", new { area = "Public" });
        }

        
    }
}
