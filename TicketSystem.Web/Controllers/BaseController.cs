using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using TicketSystem.Web.Enums;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        public UserInfo UserInfo
        {
            get
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return GetUsuerInfo();
                }

                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        private UserInfo GetUsuerInfo()
        {
            var user = new UserInfo()
            {
                UserName = User.Identity.Name,
                Role = Enum.Parse<RoleEnum>(GetUserRoleValue())
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
