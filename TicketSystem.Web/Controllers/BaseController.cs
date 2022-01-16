using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Controllers
{
    public class BaseController : ControllerBase
   {       
        public UserInfo UserInfo
        {
            get
            {
                if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
                {
                    var roleValue = this.User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Role).Value;                    
                    var user = new UserInfo()
                    {
                        UserName = User.Identity.Name,
                        Role = UserRole(roleValue)
                    };
                    return user;
                }
                return null;
            }
        }   
        
        private RoleEnum UserRole(string role)
        {
            switch (role)
            {
                case "QA":
                    return RoleEnum.QA;
                case "RD":
                    return RoleEnum.RD;
                case "PM":
                    return RoleEnum.PM;
                default:
                    return RoleEnum.Admin;
            }
        }
    }
}
