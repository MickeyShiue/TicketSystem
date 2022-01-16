using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Web.Common;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Service.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly IMemoryCache _memoryCache;

        public TicketService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public IEnumerable<TicketInfo> GetTickets(UserInfo user)
        {
            _memoryCache.TryGetValue(CacheKey.TicketList, out List<TicketInfo> tickets);
            return tickets;            
        }
    }
}
