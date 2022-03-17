using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Web.Enums;
using TicketSystem.Web.Model;
using TicketSystem.Web.Model.ResponseModel;

namespace TicketSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        [Authorize(Roles = "QA,RD")]
        [HttpGet("UserRole")]
        public ActionResult<BaseResult<RoleEnum>> GetUserRole()
        {
            if (UserInfo == null)
            {
                return new BaseResult<RoleEnum>(
                    (int)ApiResponseCodeEnum.Unauthorized,
                    ApiResponseCodeEnum.Unauthorized.ToString(),
                    RoleEnum.Default);
            }

            return new BaseResult<RoleEnum>(
                (int)ApiResponseCodeEnum.Success, 
                ApiResponseCodeEnum.Success.ToString(),
                UserInfo.Role);
        }
    }
}
