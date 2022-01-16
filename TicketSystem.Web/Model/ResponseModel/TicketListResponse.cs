using System.Collections.Generic;

namespace TicketSystem.Web.Model.ResponseModel
{
    public class TicketListResponse
    {
        public IEnumerable<TicketInfo> Tickets { get; set; }
    }
}
