using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected UserInfo UserInfo
        {
            get
            {
                if (User.Identity is { IsAuthenticated: true })
                {
                    return GetUserInfo();
                }

                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        private UserInfo GetUserInfo()
        {
            var user = new UserInfo()
            {
                UserName = User.Identity.Name,
                Role = System.Enum.Parse<RoleEnum>(GetUserRoleValue())
            };

            return user;
        }

        private string GetUserRoleValue()
        {
            return User.Claims.Where(r => r.Type == ClaimTypes.Role)
                                                   .Select(r => r.Value)
                                                   .FirstOrDefault();
        }
    }
}
