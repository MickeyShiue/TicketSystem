using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using TicketSystem.Web.Common;
using TicketSystem.Web.Model;
using TicketSystem.Web.Model.RequestModel;
using System.Linq;
using TicketSystem.Web.Enum;

namespace TicketSystem.Web.Service.AuthUserService
{
    public class AuthService : IAuthService
    {
        private readonly IMemoryCache _memoryCache;

        public AuthService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            this.InitializationCache();
        }

        public bool VerifyUser(LoginRequest login)
        {
            _memoryCache.TryGetValue(CacheKey.MemberList, out List<UserInfo> memberList);
            return memberList.Where(r => r.UserName == login.Username && r.PassWord == login.Password).Any();            
        }

        private void InitializationCache()
        {
            List<UserInfo> memberList = GetUsersInfo();
            _memoryCache.Set(CacheKey.MemberList, memberList);

            List<TicketInfo> ticketList = GetTickets();
            _memoryCache.Set(CacheKey.TicketList, ticketList);

            List<TicketStatusOption> ticketOption = GetTicketOptions();
            _memoryCache.Set(CacheKey.TicketStatusOption, ticketOption);
        }

        private static List<TicketStatusOption> GetTicketOptions()
        {
            return new List<TicketStatusOption>()
            {
                new TicketStatusOption
                {
                    id = 1,
                    value ="New"
                },
                new TicketStatusOption
                {
                    id = 2,
                    value ="Active"
                },
                new TicketStatusOption
                {
                    id = 3,
                    value ="Resloved"
                },
                new TicketStatusOption
                {
                    id = 4,
                    value ="Closed"
                },
            };
        }

        private static List<TicketInfo> GetTickets()
        {

            //tikcet
            return new List<TicketInfo>()
            {
                new TicketInfo
                {
                    TicketId = Guid.NewGuid().ToString(),
                    Title = "QA BUG1",
                    Description = "QA Bug ticket1",
                    TicketStatus = TicketStatusEnum.New,
                    TicketType = TicketTypeEnum.Bug,
                },
                new TicketInfo
                {
                    TicketId = Guid.NewGuid().ToString(),
                    Title = "QA BUG2",
                    Description = "QA Bug ticket2",
                    TicketStatus = TicketStatusEnum.Active,
                    TicketType = TicketTypeEnum.Bug,
                }
            };
        }

        private static List<UserInfo> GetUsersInfo()
        {
            //UserRole
            return new List<UserInfo>()
            {
                new UserInfo
                {
                    UserName = "QA1",
                    PassWord = "QA1",
                    Role = RoleEnum.QA,
                    TicketStatuses = new List<TicketStatusEnum>(){ TicketStatusEnum.New,TicketStatusEnum.Active,TicketStatusEnum.Closed},
                    TicketTypes = new List<TicketTypeEnum>(){ TicketTypeEnum.Bug, TicketTypeEnum.Test}
                },
                new UserInfo
                {
                    UserName = "RD1",
                    PassWord = "RD1",
                    Role = RoleEnum.RD,
                    TicketStatuses = new List<TicketStatusEnum>(){ TicketStatusEnum .Resolved},
                    TicketTypes = new List<TicketTypeEnum>(){}
                },
            };
        }
    }
}
