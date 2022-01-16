using System.Collections.Generic;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Service.TicketService
{
    public interface ITicketService
    {
        IEnumerable<TicketInfo> GetTickets(UserInfo user);
    }
}
