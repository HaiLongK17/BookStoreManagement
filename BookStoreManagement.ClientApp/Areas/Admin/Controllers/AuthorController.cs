
using AspNetCoreHero.ToastNotification.Abstractions;
using BlogManagement.UI.Extensions;
using BookStoreManagement.ClientApp.Helpers;
using BookStoreManagement.ClientApp.Models;
using BookStoreManagement.ClientApp.Models.DTO;
using BookStoreManagement.ClientApp.Models.Enums;
using BookStoreManagement.ClientApp.Models.ViewModels;
using BookStoreManagement.ClientApp.Services.ModelService;
using BookStoreManagement.ClientApp.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly string _jwt;
        public AuthorController(IHttpClientFactory clientFactory, INotyfService notyf,
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

            List<AuthorDto> authorsDto = await GetAuthorsDto();

            return View(authorsDto);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new AuthorViewModel());
            else
            {
                AuthorDto authorDto = await GetAuthorDto(id);

                var authorVM = new AuthorViewModel
                {
                    Author = authorDto,
                    HandleAuthor = new HandleAuthor
                    {
                        Name = authorDto.Name,
                        Description = authorDto.Description
                    }
                };

                return View(authorVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, AuthorViewModel authorVM)
        {
            string message = string.Empty;

            if (id == 0)
            {
                CreateAuthorDto createAuthor = new()
                {
                    Name = authorVM.HandleAuthor.Name,
                    Description = authorVM.HandleAuthor.Description
                };

                var authorContent = new StringContent(JsonConvert.SerializeObject(createAuthor), Encoding.UTF8, "application/json");
                var addRequest = await _client.PostAsync("/api/authors", authorContent);
                if ((int)addRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    addRequest = await _client.PostAsync("/api/authors", authorContent);
                }
                string authorJsonData = await addRequest.Content.ReadAsStringAsync();
                Response<AuthorDto> response = JsonConvert.DeserializeObject<Response<AuthorDto>>(authorJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", authorVM)
                    });
                }
                message = "Add new Author successfully";
            }
            else
            {
                var modelChanged =
                   ModelService<AuthorDto, HandleAuthor>.ModelChanged(authorVM.Author, authorVM.HandleAuthor);

                if (!modelChanged)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = "You have not changed anything",
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", authorVM)
                    });
                }

                UpdateAuthorDto updateAuthor = new()
                {
                    Name = authorVM.HandleAuthor.Name,
                    Description = authorVM.HandleAuthor.Description
                };
                var authorContent = new StringContent(JsonConvert.SerializeObject(updateAuthor), Encoding.UTF8, "application/json");
                var updateRequest = await _client.PutAsync($"/api/authors/{id}", authorContent);

                if ((int)updateRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    updateRequest = await _client.PutAsync($"/api/authors/{id}", authorContent);
                }
                string authorJsonData = await updateRequest.Content.ReadAsStringAsync();
                Response<int> response = JsonConvert.DeserializeObject<Response<int>>(authorJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", authorVM)
                    });
                }

                message = "Update author successfully";
            }
            List<AuthorDto> authorsDto = await GetAuthorsDto();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = message,
                html = RenderHelper.RenderRazorViewToString(this, "_AuthorContent", authorsDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            AuthorDto authorDto = await GetAuthorDto(id);

            var authorVM = new AuthorViewModel
            {
                Author = authorDto
            };

            return View(authorVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, AuthorViewModel authorVM)
        {
            var deleteRequest = await _client.DeleteAsync($"/api/authors/{id}");

            if ((int)deleteRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                deleteRequest = await _client.DeleteAsync($"/api/authors/{id}");
            }

            string authorJsonData = await deleteRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(authorJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Delete", authorVM)
                });
            }
            List<AuthorDto> authorsDto = await GetAuthorsDto();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = "Delete author successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_AuthorContent", authorsDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Detail(int id)
        {
            AuthorDto authorDto = await GetAuthorDto(id);

            var authorVM = new AuthorViewModel
            {
                Author = authorDto
            };

            return View(authorVM);
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

        private async Task<AuthorDto> GetAuthorDto(int id)
        {
            var authorResponse = await _client.GetAsync($"/api/authors/{id}");
            string authorJsonData = await authorResponse.Content.ReadAsStringAsync();
            AuthorDto authorDto = JsonConvert.DeserializeObject<Response<AuthorDto>>(authorJsonData).Data;

            return authorDto;
        }

        private async Task<List<AuthorDto>> GetAuthorsDto()
        {
            var authorsResponse = await _client.GetAsync("/api/authors");
            string authorsJsonData = await authorsResponse.Content.ReadAsStringAsync();
            List<AuthorDto> authorsDto = JsonConvert.DeserializeObject<Response<List<AuthorDto>>>(authorsJsonData).Data;

            return authorsDto;
        }
    }
}
