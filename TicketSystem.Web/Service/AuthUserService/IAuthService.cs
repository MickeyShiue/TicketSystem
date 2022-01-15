using TicketSystem.Web.Model.RequestModel;

namespace TicketSystem.Web.Service.AuthUserService
{
    public interface IAuthService
    {
        bool VerifyUser(LoginRequest login);
    }
}
