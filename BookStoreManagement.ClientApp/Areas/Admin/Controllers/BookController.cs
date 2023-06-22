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
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly string _jwt;
        public BookController(IHttpClientFactory clientFactory, INotyfService notyf,
            IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _clientFactory = clientFactory;
            _notyf = notyf;
            _client = _clientFactory.CreateClient("BookStoreClient");
            _userService = userService;
            _jwt = contextAccessor.HttpContext?.Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var currentUser = await _userService.GetUser();

            if (currentUser == null || currentUser.Role != Roles.SiteOwner.ToString()) return UnAuthorized();

            PaginatedResult<List<BookDto>> booksDto = await GetBooksDto(pageNumber);

            return View(booksDto);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0, int currentPage = 1)
        {
            var additionData = await InitAdditionViewModel();

            if (id == 0) return View(new BookViewModel { Addition = additionData });

            else
            {
                BookDto bookDto = await GetBookDto(id);

                var bookVM = new BookViewModel
                {
                    Id = bookDto.Id,
                    PhotoUrl = bookDto.PhotoUrl,
                    CurrentPage = currentPage,
                    Addition = additionData
                };

                bookVM.CurrentBook = bookVM.HandleBook = new HandleBook
                {
                    Title = bookDto.Title,
                    Summary = bookDto.Summary,
                    Price = bookDto.Price,
                    Quantity = bookDto.Quantity,
                    IsActice = bookDto.IsActice,
                    AuthorId = bookDto.AuthorDto.Id,
                    PublisherId = bookDto.PublisherDto.Id,
                    CategoryIds = bookDto.Categories.Select(x => x.Id).ToArray()
                };

                return View(bookVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, BookViewModel bookVM)
        {
            string message = string.Empty;
            bookVM.Addition = await InitAdditionViewModel();

            if (id == 0)
            {
                CreateBookDto createBook = new()
                {
                    File = bookVM.HandleBook.File
                };

                MultipartFormDataContent createBookForm = BookForm(bookVM);

                var addRequest = await _client.PostAsync("/api/books", createBookForm);

                if ((int)addRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    addRequest = await _client.PostAsync("/api/books", createBookForm);
                }

                string bookJsonData = await addRequest.Content.ReadAsStringAsync();
                Response<BookDto> response = JsonConvert.DeserializeObject<Response<BookDto>>(bookJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", bookVM)
                    });
                }
                message = "Add new Book successfully";
            }
            else
            {
                var modelChanged =
                   ModelService<HandleBook, HandleBook>.ModelChanged(bookVM.CurrentBook, bookVM.HandleBook);

                if (!modelChanged)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = "You have not changed anything",
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", bookVM)
                    });
                }

                MultipartFormDataContent updateBookForm = BookForm(bookVM);

                var updateRequest = await _client.PutAsync($"/api/books/{id}", updateBookForm);

                if ((int)updateRequest.StatusCode == 401)
                {
                    var refreshed = await RefreshToken();

                    if (!refreshed) return UnAuthorized();

                    updateRequest = await _client.PutAsync($"/api/books/{id}", updateBookForm);
                }
                string bookJsonData = await updateRequest.Content.ReadAsStringAsync();
                Response<int> response = JsonConvert.DeserializeObject<Response<int>>(bookJsonData);

                if (response.StatusCode != 200)
                {
                    return Json(new
                    {
                        isValid = false,
                        errors = string.Join(", ", response.Errors),
                        message = message,
                        html = RenderHelper.RenderRazorViewToString(this, "AddOrEdit", bookVM)
                    });
                }

                message = "Update book successfully";
            }
            PaginatedResult<List<BookDto>> booksDto = await GetBooksDto(bookVM.CurrentPage);
            return Json(new
            {
                isValid = true,
                errors = "",
                message = message,
                html = RenderHelper.RenderRazorViewToString(this, "_BookContent", booksDto)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            BookDto bookDto = await GetBookDto(id);

            var bookVM = new BookViewModel
            {
                Id = bookDto.Id
            };

            bookVM.CurrentBook = new HandleBook
            {
                Title = bookDto.Title
            };

            return View(bookVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, BookViewModel bookVM)
        {
            var deleteRequest = await _client.DeleteAsync($"/api/books/{id}");

            if ((int)deleteRequest.StatusCode == 401)
            {
                var refreshed = await RefreshToken();

                if (!refreshed) return UnAuthorized();

                deleteRequest = await _client.DeleteAsync($"/api/books/{id}");
            }

            string bookJsonData = await deleteRequest.Content.ReadAsStringAsync();
            Response<int> response = JsonConvert.DeserializeObject<Response<int>>(bookJsonData);

            if (response.StatusCode != 200)
            {
                return Json(new
                {
                    isValid = false,
                    errors = string.Join(", ", response.Errors),
                    message = "",
                    html = RenderHelper.RenderRazorViewToString(this, "Delete", bookVM)
                });
            }

            PaginatedResult<List<BookDto>> booksDto = await GetBooksDto(1);
            return Json(new
            {
                isValid = true,
                errors = "",
                message = "Delete book successfully",
                html = RenderHelper.RenderRazorViewToString(this, "_BookContent", booksDto.Data)
            });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Detail(int id)
        {
            var additionData = await InitAdditionViewModel();

            BookDto bookDto = await GetBookDto(id);

            var bookVM = new BookViewModel
            {
                Id = bookDto.Id,
                PhotoUrl = bookDto.PhotoUrl,
                CreatedAt = bookDto.CreatedDate,
                LastModifiedAt = bookDto.ModifiedDate,
                Addition = additionData
            };

            bookVM.CurrentBook = new HandleBook
            {
                Title = bookDto.Title,
                Summary = bookDto.Summary,
                Price = bookDto.Price,
                Quantity = bookDto.Quantity,
                IsActice = bookDto.IsActice,
                AuthorId = bookDto.AuthorDto.Id,
                PublisherId = bookDto.PublisherDto.Id,
                CategoryIds = bookDto.Categories.Select(x => x.Id).ToArray()
            };

            return View(bookVM);
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

        private async Task<AdditionViewModel> InitAdditionViewModel()
        {
            var result = new AdditionViewModel();
            var authorsResponse = await _client.GetAsync("/api/authors");
            var publishersResponse = await _client.GetAsync("/api/publishers");
            var categoriesResponse = await _client.GetAsync("/api/categories");

            string authorsJsonData = await authorsResponse.Content.ReadAsStringAsync();
            string publishersJsonData = await publishersResponse.Content.ReadAsStringAsync();
            string categoriesJsonData = await categoriesResponse.Content.ReadAsStringAsync();

            result.Authors = JsonConvert.DeserializeObject<Response<List<AuthorDto>>>(authorsJsonData).Data;
            result.Publishers = JsonConvert.DeserializeObject<Response<List<PublisherDto>>>(publishersJsonData).Data;
            result.Categories = JsonConvert.DeserializeObject<Response<List<CategoryDto>>>(categoriesJsonData).Data;

            return result;
        }

        private async Task<BookDto> GetBookDto(int id)
        {
            var bookResponse = await _client.GetAsync($"/api/books/{id}");
            string bookJsonData = await bookResponse.Content.ReadAsStringAsync();
            BookDto bookDto = JsonConvert.DeserializeObject<Response<BookDto>>(bookJsonData).Data;

            return bookDto;
        }

        private async Task<PaginatedResult<List<BookDto>>> GetBooksDto(int pageNumber)
        {
            var booksResponse = await _client.GetAsync($"/api/books?pageNumber={pageNumber}");
            string paginationData = booksResponse.Headers.GetValues("pagination").First();
            string booksJsonData = await booksResponse.Content.ReadAsStringAsync();
            Pagination pagination = JsonConvert.DeserializeObject<Pagination>(paginationData);
            List<BookDto> booksDto = JsonConvert.DeserializeObject<Response<List<BookDto>>>(booksJsonData).Data;

            var result = new PaginatedResult<List<BookDto>>(booksDto, pagination);

            return result;
        }

        private static MultipartFormDataContent BookForm(BookViewModel bookVM)
        {
            MultipartFormDataContent bookForm = new();

            bookForm.Add(new StringContent(bookVM.HandleBook.Title), "Title");
            bookForm.Add(new StringContent(bookVM.HandleBook.Summary), "Summary");
            bookForm.Add(new StringContent(bookVM.HandleBook.IsActice.ToString()), "IsActice");
            bookForm.Add(new StringContent(bookVM.HandleBook.Quantity.ToString()), "Quantity");
            bookForm.Add(new StringContent(bookVM.HandleBook.Price.ToString()), "Price");
            bookForm.Add(new StringContent(bookVM.HandleBook.AuthorId.ToString()), "AuthorId");
            bookForm.Add(new StringContent(bookVM.HandleBook.PublisherId.ToString()), "PublisherId");

            for (var i = 0 ; i < bookVM.HandleBook.CategoryIds.Length ; i++)
            {
                bookForm.Add(new StringContent(bookVM.HandleBook.CategoryIds[i].ToString()), $"CategoryIds[{i}]");
            }

            if (bookVM.HandleBook.File != null)
            {
                var fileContent = new StreamContent(bookVM.HandleBook.File.OpenReadStream())
                {
                    Headers =
                        {
                            ContentLength = bookVM.HandleBook.File.Length,
                            ContentType = new MediaTypeHeaderValue(bookVM.HandleBook.File.ContentType)
                        }
                };

                bookForm.Add(fileContent, "File", bookVM.HandleBook.File.FileName);
            }

            return bookForm;
        }
    }
}
