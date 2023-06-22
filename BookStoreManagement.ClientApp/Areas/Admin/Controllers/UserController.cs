using AspNetCoreHero.ToastNotification.Abstractions;
using BookStoreManagement.ClientApp.Services.UserService;
using BookStoreManagement.ClientApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreManagement.ClientApp.Models.Enums;
using Newtonsoft.Json;
using BookStoreManagement.ClientApp.Models;
using BookStoreManagement.ClientApp.Models.ViewModels;
using BlogManagement.UI.Extensions;
using System.Text;
using BookStoreManagement.ClientApp.Helpers;
using System;
using BookStoreManagement.ClientApp.Services.ModelService;

namespace BookStoreManagement.ClientApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly string _jwt;
        public UserController(IHttpClientFactory clientFactory, INotyfService notyf,
            IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _clientFactory = clientFactory;
            _notyf = notyf;
            _client = _clientFactory.CreateClient("BookStoreClient");
            _userService = userService;
            _jwt = contextAccessor.HttpContext?.Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userService.GetUser();

            if (currentUser == null || currentUser.Role != Roles.SiteOwner.ToString()) return UnAuthorized();

            var users = await GetUsers();

            if(users == null) return UnAuthorized();

            return View(users);
        }

        [NoDirectAccess]
        public IActionResult Add()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel userVM)
        {
            CreateUserDto createUser = new()
            {
                DisplayName = userVM.NewUser.DisplayName,
                Email = userVM.NewUser.Email,
                PhoneNumber = userVM.NewUser.PhoneNumber,
                Password = userVM.NewUser.Password,
                Role = userVM.NewUser.Role
            };
            var userContent = new StringContent(JsonConvert.SerializeObject(createUser), Encoding.UTF8, "application/json");
            var addRequest = await _client.PostAsync("/api/users", userContent);
            if ((int)addRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                addRequest = await _client.PostAsync("/api/users", userContent);
            }
            string userJsonData = await addRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(userJsonData);
            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Add", userVM)
                });
            }
            var users = await GetUsers();
            if (users == null) return UnAuthorized();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = "Add new user successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_UserContent", users)
            });
        }

        [HttpPost]
        public async Task<IActionResult> LockOrUnlock(Guid id, string displayName, bool isLocked)
        {
            string mes = string.Empty;
            var users = await GetUsers();
            if (users == null) return UnAuthorized();
            var userIdContent = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            HttpResponseMessage request;
            if (isLocked)
            {
                request = await _client.PostAsync($"/api/users/{id}/lock", userIdContent);
                if ((int)request.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    request = await _client.PostAsync($"/api/users/{id}/lock", userIdContent);
                }
                mes = $"Locked {displayName} successfully";
            }
            else
            {
                request = await _client.PostAsync($"/api/users/{id}/unlock", userIdContent);
                if ((int)request.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    request = await _client.PostAsync($"/api/users/{id}/unlock", userIdContent);
                }
                mes = $"Unlocked {displayName} successfully";
            }
            string lockJsonData = await request.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(lockJsonData);
            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "_UserContent", users)
                });
            }
            users = await GetUsers();
            if (users == null) return UnAuthorized();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = mes,
                html = RenderHelper.RenderRazorViewToString(this, "_UserContent", users)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await GetUser(id);

            if (user == null)
            {
                _notyf.Error("Could not find any resource with the requested ID");
                return RedirectToAction(nameof(Index));
            }

            var userVM = new UserViewModel
            {
                User = user,
                UpdateUser = new UpdateUserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role
                }
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UserViewModel userVM)
        {
            var modelChanged =
                   ModelService<UserDto, UpdateUserDto>.ModelChanged(userVM.User, userVM.UpdateUser);

            if (!modelChanged)
            {
                return Json(new
                {
                    isValid = false,
                    errors = "You have not changed anything",
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Edit", userVM)
                });
            }

            var userContent = UserForm(userVM);
            var updateRequest = await _client.PutAsync($"/api/users/{id}", userContent);
            if ((int)updateRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                updateRequest = await _client.PutAsync($"/api/users/{id}", userContent);
            }
            string userJsonData = await updateRequest.Content.ReadAsStringAsync();
            Response<UserDto> response = JsonConvert.DeserializeObject<Response<UserDto>>(userJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Edit", userVM)
                });
            }

            var users = await GetUsers();
            if (users == null) return UnAuthorized();

            return Json(new
            {
                isValid = true,
                errors = "",
                message = $"Update user {userVM.User.DisplayName} successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_UserContent", users)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> ChangePassword(Guid id)
        {
            var user = await GetUser(id);

            if (user == null)
            {
                _notyf.Error("Could not find any resource with the requested ID");
                return RedirectToAction(nameof(Index));
            }

            var userVM = new UserViewModel
            {
                User = user
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(Guid id, UserViewModel userVM)
        {
            var userContent = new StringContent(JsonConvert.SerializeObject(userVM.ChangePassword), Encoding.UTF8, "application/json");
            var updateRequest = await _client.PostAsync($"/api/users/{id}/password", userContent);
            if ((int)updateRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                updateRequest = await _client.PostAsync($"/api/users/{id}/password", userContent);
            }
            string userJsonData = await updateRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(userJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "ChangePassword", userVM)
                });
            }

            var users = await GetUsers();
            if (users == null) return UnAuthorized();

            return Json(new
            {
                isValid = true,
                errors = "",
                message = $"Change password for {userVM.User.DisplayName} successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_UserContent", users)
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

        private async Task<List<UserDto>> GetUsers()
        {
            string role = Roles.NormalUser.ToString();
            var usersResponse = await _client.GetAsync($"/api/users/role/{role}");
            if((int)usersResponse.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return null;

                usersResponse = await _client.GetAsync($"/api/users/role/{role}");
            }
            string usersJsonData = await usersResponse.Content.ReadAsStringAsync();
            List<UserDto> usersDto = JsonConvert.DeserializeObject<Response<List<UserDto>>>(usersJsonData).Data;

            return usersDto;
        }

        private async Task<UserDto> GetUser(Guid id)
        {
            var userResponse = await _client.GetAsync($"/api/users/{id}");
            string userJsonData = await userResponse.Content.ReadAsStringAsync();
            UserDto userDto = JsonConvert.DeserializeObject<Response<UserDto>>(userJsonData).Data;

            return userDto;
        }

        private static MultipartFormDataContent UserForm(UserViewModel userVM)
        {
            MultipartFormDataContent userForm = new();

            userForm.Add(new StringContent(userVM.UpdateUser.DisplayName), "DisplayName");
            userForm.Add(new StringContent(userVM.UpdateUser.PhoneNumber), "PhoneNumber");
            userForm.Add(new StringContent(userVM.UpdateUser.Email), "Email");
            userForm.Add(new StringContent(userVM.UpdateUser.Role), "Role");

            if (userVM.UpdateUser.File != null)
            {
                var fileContent = new StreamContent(userVM.UpdateUser.File.OpenReadStream())
                {
                    Headers =
                        {
                            ContentLength = userVM.UpdateUser.File.Length,
                            ContentType = new MediaTypeHeaderValue(userVM.UpdateUser.File.ContentType)
                        }
                };

                userForm.Add(fileContent, "File", userVM.UpdateUser.File.FileName);
            }

            return userForm;
        }
    }
}
