using UdemyJwtProjectFrontend.Builders.Concrete;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.Builders.Abstract
{
    public abstract class StatusBuilder
    {
        public abstract Status GenerateStatus(AppUser activeUser, string roles);
    }
}