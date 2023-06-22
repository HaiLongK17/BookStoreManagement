using AspNetCoreHero.ToastNotification.Abstractions;
using BookStoreManagement.ClientApp.Helpers;
using BookStoreManagement.ClientApp.Models;
using BookStoreManagement.ClientApp.Models.DTO;
using BookStoreManagement.ClientApp.Models.ViewModels;
using BookStoreManagement.ClientApp.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookStoreManagement.ClientApp.Services.UserService;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace BookStoreManagement.ClientApp.Areas.Public.Controllers
{
    [Area("Public")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly INotyfService _notyf;
        private readonly HttpClient _client;
        private readonly IUserService _userService;

        public HomeController(IHttpClientFactory clientFactory, INotyfService notyf, IUserService userService)
        {
            _userService = userService;
            _clientFactory = clientFactory;
            _notyf = notyf;
            _client = _clientFactory.CreateClient("BookStoreClient");
        }

        public async Task<IActionResult> Index()
        {
            var homeVM = await InitHomeViewModel(1);
            return View(homeVM.Data);
        }

        // public async Task<IActionResult> ShopGrid(int pageNumber = 1, string Category = null,
        //     string Author = null, string Publisher = null, string SearchBook = null)
        // {
        //     var homeVM = await InitHomeViewModel(pageNumber, Category, Author, Publisher);

        //     if(SearchBook != null)
        //     {
        //         homeVM.Data.Books = homeVM.Data.Books.Where(x => x.Title.ToLower().Contains(SearchBook.ToLower())).ToList();
        //     }

        //     return View(homeVM);
        // }

        // public async Task<IActionResult> Detail(int id)
        // {
        //     var detailVM = new DetailBookViewModel();

        //     var currentUser = await _userService.GetUser();
        //     var additionData = await InitAdditionViewModel();
        //     var bookDto = await GetBookDto(id);
        //     var commentDto = await GetCommentDto(id);

        //     detailVM.Book = bookDto;
        //     detailVM.Addition = additionData;

        //     detailVM.CommentViewModel = new CommentViewModel
        //     {
        //         Comments = commentDto,
        //         PostComment = new PostCommentDto
        //         {
        //             UserId = currentUser?.Id,
        //             BookId = bookDto.Id
        //         }
        //     };

        //     return View(detailVM);
        // }

        // [HttpPost]
        // public async Task<IActionResult> PostComment(DetailBookViewModel detailVM)
        // {
        //     var comments = await GetCommentDto(detailVM.CommentViewModel.PostComment.BookId);

        //     var commentContent = new StringContent(JsonConvert.SerializeObject(detailVM.CommentViewModel.PostComment), Encoding.UTF8, "application/json");
        //     var addRequest = await _client.PostAsync("/api/comments", commentContent);
        //     string commentJsonData = await addRequest.Content.ReadAsStringAsync();
        //     Response<int> response = JsonConvert.DeserializeObject<Response<int>>(commentJsonData);

        //     if (response.StatusCode != 200)
        //     {
        //         return Json(new
        //         {
        //             isValid = false,
        //             errors = string.Join(", ", response.Errors),
        //             html = RenderHelper.RenderRazorViewToString(this, "_CommentPartial", comments)
        //         });
        //     }

        //     comments = await GetCommentDto(detailVM.CommentViewModel.PostComment.BookId);

        //     return Json(new
        //     {
        //         isValid = true,
        //         errors = "",
        //         html = RenderHelper.RenderRazorViewToString(this, "_CommentPartial", comments)
        //     });
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<PaginatedResult<HomeViewModel>> InitHomeViewModel(int pageNumber,
            string Category = null, string Author = null, string Publisher = null)
        {
            UriBuilder builder = new();

            string filter = string.Empty;

            var homeVM = new HomeViewModel();

            if (!string.IsNullOrEmpty(Category))
            {
                filter += $"&category={Category.Replace("&", "")}";
            }
            if (!string.IsNullOrEmpty(Author))
            {
                filter += $"&author={Author}";
            }
            if (!string.IsNullOrEmpty(Publisher))
            {
                filter += $"&publisher={Publisher}";
            }

            builder.Query = $"pageNumber={pageNumber}{filter}";

            var booksResponse =
                await _client.GetAsync($"/api/books/{builder.Query}");
            string paginationData = booksResponse.Headers.GetValues("pagination").First();
            string booksJsonData = await booksResponse.Content.ReadAsStringAsync();
            Pagination pagination = JsonConvert.DeserializeObject<Pagination>(paginationData);

            homeVM.Books = JsonConvert.DeserializeObject<Response<List<BookDto>>>(booksJsonData).Data;
            homeVM.Addition = await InitAdditionViewModel();

            var result = new PaginatedResult<HomeViewModel>(homeVM, pagination);

            return result;
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

        // private async Task<List<CommentDto>> GetCommentDto(int bookId)
        // {
        //     var commentResponse = await _client.GetAsync($"/api/comments/book/{bookId}");
        //     string commentJsonData = await commentResponse.Content.ReadAsStringAsync();

        //     var result = JsonConvert.DeserializeObject<Response<List<CommentDto>>>(commentJsonData).Data;

        //     return result;
        // }

        // private async Task<BookDto> GetBookDto(int id)
        // {
        //     var bookResponse = await _client.GetAsync($"/api/books/{id}");
        //     string bookJsonData = await bookResponse.Content.ReadAsStringAsync();
        //     var bookDto = JsonConvert.DeserializeObject<Response<BookDto>>(bookJsonData).Data;
        //     return bookDto;
        // }
    }
}
