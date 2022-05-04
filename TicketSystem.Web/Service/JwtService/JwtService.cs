using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSystem.Web.Common;
using TicketSystem.Web.Model;
using TicketSystem.Web.Model.RequestModel;
using System.Linq;
using TicketSystem.Web.Enum;

namespace TicketSystem.Web.Service.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration Configuration;
        private readonly IMemoryCache _memoryCache;

        public JwtService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            this.Configuration = configuration;
            this._memoryCache = memoryCache;          
        }

        public string GenerateToken(LoginRequest login, int expireMinutes = 30)
        {
            var issuer = Configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = Configuration.GetValue<string>("JwtSettings:SignKey");

            // 設定要加入到 JWT Token 中的聲明資訊(Claims)
            var claims = new List<Claim>();


            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, login.Username)); // User.Identity.Name

            // 你可以自行擴充 "roles" 加入登入者該有的角色
            claims.Add(new Claim("roles", GetRole(login)));            
            var userClaimsIdentity = new ClaimsIdentity(claims);

            // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // 建立 SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = signingCredentials
            };

            // 產出所需要的 JWT securityToken 物件，並取得序列化後的 Token 結果(字串格式)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            return serializeToken;
        }

        private string GetRole(LoginRequest login)
        {
            _memoryCache.TryGetValue(CacheKey.MemberList, out List<UserInfo> memberList);

            var role = memberList.FirstOrDefault(r => r.UserName == login.Username).Role;

            switch (role)
            {
                case RoleEnum.QA:
                    return "QA";                   
                case RoleEnum.RD:
                    return "RD";
                case RoleEnum.PM:
                    return "PM";
                case RoleEnum.Admin:
                    return "Admin";
                default:
                    return "NotFound";
            }
        }
    }
}
