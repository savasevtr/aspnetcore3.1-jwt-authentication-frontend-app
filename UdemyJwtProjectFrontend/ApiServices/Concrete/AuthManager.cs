using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdemyJwtProjectFrontend.ApiServices.Interfaces;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.ApiServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(AppUserLogin appUserLogin)
        {
            var jsonData = JsonConvert.SerializeObject(appUserLogin);

            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
         
            var responseMessage = await httpClient.PostAsync("http://localhost:60531/api/auth/SignIn", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(await responseMessage.Content.ReadAsStringAsync());

                _httpContextAccessor.HttpContext.Session.SetString("token", token.Token);

                return true;
            }

            return false;
        }

        public void LogOut()
        {
            _httpContextAccessor.HttpContext.Session.Remove("token");
        }
    }
}