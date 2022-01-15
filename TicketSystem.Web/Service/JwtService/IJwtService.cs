using TicketSystem.Web.Model.RequestModel;

namespace TicketSystem.Web.Service.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(LoginRequest login, int expireMinutes = 30);
    }
}
