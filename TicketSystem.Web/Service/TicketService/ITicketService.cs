using System.Collections.Generic;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model;

namespace TicketSystem.Web.Service.TicketService
{
    public interface ITicketService
    {
        IEnumerable<TicketInfo> GetTickets();

        TicketInfo GetTicketById(string ticketId);

        IEnumerable<TicketStatusOption> GetTicketOptionsByRole(RoleEnum role);

        bool UpdateTicket(TicketInfo ticket, RoleEnum role);

        bool DeleteTicket(TicketInfo ticket);

        bool CreateTicket(TicketInfo ticket);
    }
}
