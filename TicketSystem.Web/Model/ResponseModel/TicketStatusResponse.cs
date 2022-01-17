using System.Collections.Generic;

namespace TicketSystem.Web.Model.ResponseModel
{
    public class TicketStatusResponse
    {
        public IEnumerable<TicketStatusOption> TicketStatusOptions { get; set; }
    }
}
