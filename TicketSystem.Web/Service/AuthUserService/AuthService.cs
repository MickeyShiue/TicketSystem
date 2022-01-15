﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using TicketSystem.Web.Common;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model;
using TicketSystem.Web.Model.RequestModel;
using System.Linq;

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
            var isExist = memberList.Where(r => r.UserName == login.Username && r.PassWord == login.Password).Any();
            return isExist;
        }

        private void InitializationCache()
        {
            //UserRole
            var memberList = new List<UserInfo>()
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
                    TicketStatuses = new List<TicketStatusEnum>(){ TicketStatusEnum .Resloved},
                    TicketTypes = new List<TicketTypeEnum>(){}
                },
            };
            _memoryCache.Set(CacheKey.MemberList, memberList);
        }
    }
}