using AspNetCoreHero.ToastNotification.Abstractions;
using BookStoreManagement.ClientApp.Models.Enums;
using BookStoreManagement.ClientApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;
        public DashboardController(IUserService userService, INotyfService notyf)
        {
            _userService = userService;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userService.GetUser();

            if (currentUser == null || currentUser.Role != Roles.SiteOwner.ToString())
            {
                
                return RedirectToAction("Login", "Auth", new { area = "Authentication" });
            }

            return View();
        }
    }
}
