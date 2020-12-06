using UdemyJwtProjectFrontend.Builders.Abstract;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.Builders.Concrete
{
    public class SingleRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            Status status = new Status();

            if (activeUser.Roles.Contains(roles))
            {
                status.AccessStatus = true;
            }

            return status;
        }
    }
}