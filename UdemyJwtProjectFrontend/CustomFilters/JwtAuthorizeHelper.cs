using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using UdemyJwtProjectFrontend.Builders.Concrete;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.CustomFilters
{
    public class JwtAuthorizeHelper
    {
        public static void CheckUserRole(AppUser activeUser, string roles, ActionExecutingContext context)
        {
            if (!string.IsNullOrWhiteSpace(roles))
            {
                Status status = null;

                if (roles.Contains(","))
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status =  director.GenerateStatus(activeUser, roles);
                }
                else
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }

                CheckStatus(status, context);
            }
        }

        private static void CheckStatus(Status status, ActionExecutingContext context)
        {
            if (!status.AccessStatus)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
        }

        public static AppUser GetActiveUser(HttpResponseMessage httpResponseMessage)
        {
            return JsonConvert.DeserializeObject<AppUser>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }

        public static bool CheckToken(ActionExecutingContext context, out string token)
        {
            token = context.HttpContext.Session.GetString("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                return true;
            }

            context.Result = new RedirectToActionResult("SignIn", "Account", null);

            return false;
        }

        public static HttpResponseMessage GetActiveUserResponseMessage(string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient.GetAsync("http://localhost:60531/api/Auth/ActiveUser").Result;
        }
    }
}