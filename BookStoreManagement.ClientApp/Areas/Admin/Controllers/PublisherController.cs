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
    public class PublisherController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly string _jwt;
        public PublisherController(IHttpClientFactory clientFactory, INotyfService notyf,
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

            List<PublisherDto> publishersDto = await GetPublishersDto();

            return View(publishersDto);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new PublisherViewModel());
            else
            {
                PublisherDto publisherDto = await GetPublisherDto(id);

                var publisherVM = new PublisherViewModel
                {
                    Publisher = publisherDto,
                    HandlePublisher = new HandlePublisher
                    {
                        Name = publisherDto.Name,
                        Description = publisherDto.Description
                    }
                };

                return View(publisherVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, PublisherViewModel publisherVM)
        {
            string message = string.Empty;

            if (id == 0)
            {
                CreatePublisherDto createPublisher = new()
                {
                    Name = publisherVM.HandlePublisher.Name,
                    Description = publisherVM.HandlePublisher.Description
                };

                var publisherContent = new StringContent(JsonConvert.SerializeObject(createPublisher), Encoding.UTF8, "application/json");
                var addRequest = await _client.PostAsync("/api/publishers", publisherContent);
                if ((int)addRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    addRequest = await _client.PostAsync("/api/publishers", publisherContent);
                }
                string publisherJsonData = await addRequest.Content.ReadAsStringAsync();
                Response<PublisherDto> response = JsonConvert.DeserializeObject<Response<PublisherDto>>(publisherJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", publisherVM)
                    });
                }
                message = "Add new Publisher successfully";
            }
            else
            {
                var modelChanged =
                   ModelService<PublisherDto, HandlePublisher>.ModelChanged(publisherVM.Publisher, publisherVM.HandlePublisher);

                if (!modelChanged)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = "You have not changed anything",
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", publisherVM)
                    });
                }

                UpdatePublisherDto updatePublisher = new()
                {
                    Name = publisherVM.HandlePublisher.Name,
                    Description = publisherVM.HandlePublisher.Description
                };
                var publisherContent = new StringContent(JsonConvert.SerializeObject(updatePublisher), Encoding.UTF8, "application/json");
                var updateRequest = await _client.PutAsync($"/api/publishers/{id}", publisherContent);

                if ((int)updateRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    updateRequest = await _client.PutAsync($"/api/publishers/{id}", publisherContent);
                }
                string publisherJsonData = await updateRequest.Content.ReadAsStringAsync();
                Response<int> response = JsonConvert.DeserializeObject<Response<int>>(publisherJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", publisherVM)
                    });
                }

                message = "Update publisher successfully";
            }
            List<PublisherDto> publishersDto = await GetPublishersDto();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = message,
                html = RenderHelper.RenderRazorViewToString(this, "_PublisherContent", publishersDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            PublisherDto publisherDto = await GetPublisherDto(id);

            var publisherVM = new PublisherViewModel
            {
                Publisher = publisherDto
            };

            return View(publisherVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, PublisherViewModel publisherVM)
        {
            var deleteRequest = await _client.DeleteAsync($"/api/publishers/{id}");

            if ((int)deleteRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                deleteRequest = await _client.DeleteAsync($"/api/publishers/{id}");
            }

            string publisherJsonData = await deleteRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(publisherJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Delete", publisherVM)
                });
            }
            List<PublisherDto> publishersDto = await GetPublishersDto();
            return Json(new
            {
                isValid = true,
                errors = "",
                message = "Delete publisher successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_PublisherContent", publishersDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Detail(int id)
        {
            PublisherDto publisherDto = await GetPublisherDto(id);

            var publisherVM = new PublisherViewModel
            {
                Publisher = publisherDto
            };

            return View(publisherVM);
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

        private async Task<PublisherDto> GetPublisherDto(int id)
        {
            var publisherResponse = await _client.GetAsync($"/api/publishers/{id}");
            string publisherJsonData = await publisherResponse.Content.ReadAsStringAsync();
            PublisherDto publisherDto = JsonConvert.DeserializeObject<Response<PublisherDto>>(publisherJsonData).Data;

            return publisherDto;
        }

        private async Task<List<PublisherDto>> GetPublishersDto()
        {
            var publishersResponse = await _client.GetAsync("/api/publishers");
            string publishersJsonData = await publishersResponse.Content.ReadAsStringAsync();
            List<PublisherDto> publishersDto = JsonConvert.DeserializeObject<Response<List<PublisherDto>>>(publishersJsonData).Data;

            return publishersDto;
        }
    }
}
