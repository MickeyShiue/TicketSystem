using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model.RequestModel;
using TicketSystem.Web.Model.ResponseModel;
using TicketSystem.Web.Service.JwtService;

namespace TicketSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public TokenController(IJwtService jwtService)
        {
            this._jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<BaseResult<TokenResponse>> SignIn(LoginRequest login)
        {
            var token = _jwtService.GenerateToken(login);

            if (!string.IsNullOrEmpty(token))
            {
                var tokenResponse = new TokenResponse() { Token = token };
                return new BaseResult<TokenResponse>((int)ApiResponseCode.Success, ApiResponseCode.Success.ToString(), tokenResponse);
            }

            return new BaseResult<TokenResponse>((int)ApiResponseCode.Unauthorized, ApiResponseCode.Unauthorized.ToString(), null);
        }

    }
}
