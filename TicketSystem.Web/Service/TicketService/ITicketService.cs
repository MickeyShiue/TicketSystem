using System.Collections.Generic;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Service.TicketService
{
    public interface ITicketService
    {
        IEnumerable<TicketInfo> GetTickets(UserInfo user);

        TicketInfo GetTicketById(string ticketId);

        IEnumerable<TicketStatusOption> GetTicketOptionsByRole(RoleEnum role);

        bool UpdateTicket(TicketInfo ticket);

        bool DeleteTicket(TicketInfo ticket);
    }
}
