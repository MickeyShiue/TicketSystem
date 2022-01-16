using System.Collections.Generic;
using TicketSystem.Web.Enum;

namespace TicketSystem.Web.Model
{
    public class TicketInfo
    {
        public string TicketId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TicketStatusEnum TicketStatus { get; set; }

        public TicketTypeEnum TicketType { get; set; }

        public IEnumerable<TicketStatusOption> TicketStatusOptions { get; set; }
    }    

    public class TicketStatusOption
    {
        public int id { get; set; }

        public string value { get; set; }
    }
}
