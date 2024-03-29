﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model.RequestModel;
using TicketSystem.Web.Model.ResponseModel;
using TicketSystem.Web.Service.AuthUserService;
using TicketSystem.Web.Service.JwtService;

namespace TicketSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthService _authService;

        public TokenController(IJwtService jwtService, IAuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<BaseResult<TokenResponse>> Login(LoginRequest login)
        {
            if (!_authService.VerifyUser(login))
                return new BaseResult<TokenResponse>((int)ApiResponseCodeEnum.Unauthorized, ApiResponseCodeEnum.Unauthorized.ToString(), null);
            
            var tokenResponse = new TokenResponse()
            {
                Token = _jwtService.GenerateToken(login)
            };
            
            return new BaseResult<TokenResponse>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), tokenResponse);
        }
    }
}
