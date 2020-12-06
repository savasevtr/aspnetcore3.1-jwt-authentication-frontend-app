using UdemyJwtProjectFrontend.Builders.Abstract;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.Builders.Concrete
{
    public class StatusBuilderDirector
    {
        private StatusBuilder _builder;

        public StatusBuilderDirector(StatusBuilder builder)
        {
            _builder = builder;
        }

        public Status GenerateStatus(AppUser activeUser, string roles)
        {
            return _builder.GenerateStatus(activeUser, roles);
        }
    }
}