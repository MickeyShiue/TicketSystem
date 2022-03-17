using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketSystem.Web.Common;
using TicketSystem.Web.Enums;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Service.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly IMemoryCache _memoryCache;
        private static object _lock = new object();

        public TicketService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public IEnumerable<TicketInfo> GetTickets()
        {
            _memoryCache.TryGetValue(CacheKey.TicketList, out List<TicketInfo> tickets);
            return tickets;
        }

        public TicketInfo GetTicketById(string ticketId)
        {
            _memoryCache.TryGetValue(CacheKey.TicketList, out List<TicketInfo> tickets);
            var ticket = tickets.FirstOrDefault(r => r.TicketId == ticketId);
            return ticket;
        }

        public IEnumerable<TicketStatusOption> GetTicketOptionsByRole(RoleEnum role)
        {
            _memoryCache.TryGetValue(CacheKey.TicketStatusOption, out List<TicketStatusOption> ticketStatusOptions);

            if (role == RoleEnum.QA)
            {
                return ticketStatusOptions.Where(r => r.id != (int)TicketStatusEnum.Resloved).ToList();
            }

            if (role == RoleEnum.RD)
            {
                return ticketStatusOptions.Where(r => r.id == (int)TicketStatusEnum.Resloved).ToList();
            }

            return ticketStatusOptions;
        }

        public bool UpdateTicket(TicketInfo ticket, RoleEnum role)
        {
            _memoryCache.TryGetValue(CacheKey.TicketList, out List<TicketInfo> tickets);

            var roleAllowTicketStatus = GetTicketOptionsByRole(role);
            var IsAllowUpdate = roleAllowTicketStatus.Any(r => r.id == (int)ticket.TicketStatus);
            if (IsAllowUpdate)
            {
                foreach (var ticketItem in tickets)
                {
                    if (ticketItem.TicketId == ticket.TicketId)
                    {
                        ticketItem.Title = ticket.Title;
                        ticketItem.Description = ticket.Description;
                        ticketItem.TicketStatus = ticket.TicketStatus;
                    }
                }
                lock (_lock)
                {
                    _memoryCache.Set(CacheKey.TicketList, tickets);
                    return true;
                }
              
            }

            return false;
        }

        public bool DeleteTicket(TicketInfo ticket)
        {
            _memoryCache.TryGetValue(CacheKey.TicketList, out List<TicketInfo> tickets);

            var ticketResult = tickets.Where(r => r.TicketId != ticket.TicketId).ToList();
            lock (_lock)
            {
                _memoryCache.Set(CacheKey.TicketList, ticketResult);
            }

            return true;
        }

        public bool CreateTicket(TicketInfo ticket)
        {
            _memoryCache.TryGetValue(CacheKey.TicketList, out List<TicketInfo> tickets);

            tickets.Add(new TicketInfo()
            {
                TicketId = Guid.NewGuid().ToString(),
                Title = ticket.Title,
                Description = ticket.Description,
                TicketStatus = ticket.TicketStatus,
                TicketType = TicketTypeEnum.Bug
            });

            lock (_lock)
            {
                _memoryCache.Set(CacheKey.TicketList, tickets);
            }          
            return true;
        }                  
    }
}
