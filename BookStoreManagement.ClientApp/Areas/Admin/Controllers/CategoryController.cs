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
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly string _jwt;
        public CategoryController(IHttpClientFactory clientFactory, INotyfService notyf,
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

            List<CategoryDto> categoriesDto = await GetCategoriesDto();

            return View(categoriesDto);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new CategoryViewModel());
            else
            {
                CategoryDto categoryDto = await GetCategoryDto(id);

                var categoryVM = new CategoryViewModel
                {
                    Category = categoryDto,
                    HandleCategory = new HandleCategory
                    {
                        Name = categoryDto.Name,
                        Description = categoryDto.Description
                    }
                };

                return View(categoryVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, CategoryViewModel categoryVM)
        {
            string message = string.Empty;

            if (id == 0)
            {
                CreateCategoryDto createCategory = new()
                {
                    Name = categoryVM.HandleCategory.Name,
                    Description = categoryVM.HandleCategory.Description
                };

                var categoryContent = new StringContent(JsonConvert.SerializeObject(createCategory), Encoding.UTF8, "application/json");
                var addRequest = await _client.PostAsync("/api/categories", categoryContent);
                if ((int)addRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    addRequest = await _client.PostAsync("/api/categories", categoryContent);
                }
                string categoryJsonData = await addRequest.Content.ReadAsStringAsync();
                Response<CategoryDto> response = JsonConvert.DeserializeObject<Response<CategoryDto>>(categoryJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", categoryVM)
                    });
                }
                message = "Add new Category successfully";
            }
            else
            {
                var modelChanged =
                   ModelService<CategoryDto, HandleCategory>.ModelChanged(categoryVM.Category, categoryVM.HandleCategory);

                if (!modelChanged)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = "You have not changed anything",
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", categoryVM)
                    });
                }

                UpdateCategoryDto updateCategory = new()
                {
                    Name = categoryVM.HandleCategory.Name,
                    Description = categoryVM.HandleCategory.Description
                };
                var categoryContent = new StringContent(JsonConvert.SerializeObject(updateCategory), Encoding.UTF8, "application/json");
                var updateRequest = await _client.PutAsync($"/api/categories/{id}", categoryContent);

                if ((int)updateRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    updateRequest = await _client.PutAsync($"/api/categories/{id}", categoryContent);
                }
                string categoryJsonData = await updateRequest.Content.ReadAsStringAsync();
                Response<int> response = JsonConvert.DeserializeObject<Response<int>>(categoryJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", categoryVM)
                    });
                }

                message = "Update category successfully";
            }
            List<CategoryDto> categoriesDto = await GetCategoriesDto();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = message,
                html = RenderHelper.RenderRazorViewToString(this, "_CategoryContent", categoriesDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            CategoryDto categoryDto = await GetCategoryDto(id);

            var categoryVM = new CategoryViewModel
            {
                Category = categoryDto
            };

            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CategoryViewModel categoryVM)
        {
            var deleteRequest = await _client.DeleteAsync($"/api/categories/{id}");

            if ((int)deleteRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                deleteRequest = await _client.DeleteAsync($"/api/categories/{id}");
            }

            string categoryJsonData = await deleteRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(categoryJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Delete", categoryVM)
                });
            }
            List<CategoryDto> categoriesDto = await GetCategoriesDto();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = "Delete category successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_CategoryContent", categoriesDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Detail(int id)
        {
            CategoryDto categoryDto = await GetCategoryDto(id);

            var categoryVM = new CategoryViewModel
            {
                Category = categoryDto
            };

            return View(categoryVM);
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

        private async Task<CategoryDto> GetCategoryDto(int id)
        {
            var categoryResponse = await _client.GetAsync($"/api/categories/{id}");
            string categoryJsonData = await categoryResponse.Content.ReadAsStringAsync();
            CategoryDto categoryDto = JsonConvert.DeserializeObject<Response<CategoryDto>>(categoryJsonData).Data;

            return categoryDto;
        }

        private async Task<List<CategoryDto>> GetCategoriesDto()
        {
            var categoriesResponse = await _client.GetAsync("/api/categories");
            string categoriesJsonData = await categoriesResponse.Content.ReadAsStringAsync();
            List<CategoryDto> categoriesDto = JsonConvert.DeserializeObject<Response<List<CategoryDto>>>(categoriesJsonData).Data;

            return categoriesDto;
        }
    }
}
