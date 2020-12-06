using System.Threading.Tasks;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.ApiServices.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(AppUserLogin appUserLogin);
        void LogOut();
    }
}