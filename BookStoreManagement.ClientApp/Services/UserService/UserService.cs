using BookStoreManagement.ClientApp.Models;
using BookStoreManagement.ClientApp.Models.DTO;
using BookStoreManagement.ClientApp.Services.CookieService;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;
        private readonly ICookieService _cookieService;
        public UserService(IHttpClientFactory clientFactory, ICookieService cookieService)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient("BookStoreClient");
            _cookieService = cookieService;
        }
        public async Task<UserDto> GetUser()
        {
            HttpResponseMessage response;
            StringContent credContent;
            Response<UserDto> userResponse;
            string userJsonData;

            var cookie = _cookieService.GetCookieData();

            credContent = new(JsonConvert.SerializeObject(cookie), Encoding.UTF8, "application/json");

            response = await UserResponse(credContent, cookie.Token);

            if ((int)response.StatusCode == 401)
            {
                var refreshResult = await RefreshToken();

                if (refreshResult == null) return null;

                credContent = new(JsonConvert.SerializeObject(refreshResult), Encoding.UTF8, "application/json");

                response = await UserResponse(credContent, refreshResult.Token);
                userJsonData = await response?.Content.ReadAsStringAsync();
                userResponse = JsonConvert.DeserializeObject<Response<UserDto>>(userJsonData);

                if (userResponse == null) return null;

                return userResponse.Data;
            }

            if ((int)response.StatusCode == 404 || (int)response.StatusCode == 500) return null;

            if ((int)response.StatusCode == 401) return null;

            userJsonData = await response?.Content.ReadAsStringAsync();
            userResponse = JsonConvert.DeserializeObject<Response<UserDto>>(userJsonData);

            return userResponse.Data;
        }

        private async Task<HttpResponseMessage> UserResponse(StringContent credContent, string jwt)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var currentUserResponse = await _client.PostAsync("/api/authentications", credContent);

            return currentUserResponse;
        }

        public async Task<CookieDto> RefreshToken()
        {
            var cookie = _cookieService.GetCookieData();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie.Token);

            var refreshContent = new StringContent(JsonConvert.SerializeObject(cookie), Encoding.UTF8, "application/json");

            var refreshRequest = await _client.PostAsync("/api/authentications/refresh-token", refreshContent);

            if ((int)refreshRequest.StatusCode == 401)
            {
                _cookieService.ClearCookie();
                return null;
            }

            string refreshJsonData = await refreshRequest.Content.ReadAsStringAsync();

            Response<CookieDto> response = JsonConvert.DeserializeObject<Response<CookieDto>>(refreshJsonData);

            if (response == null || response.StatusCode != 200)
            {
                _cookieService.ClearCookie();
                return null;
            }

            _cookieService.SetCookie(response.Data);

            return response.Data;
        }
    }
}
