using TicketSystem.Web.Enums;

namespace TicketSystem.Web.Model
{
    public class TicketInfo
    {
        public string TicketId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TicketStatusEnum TicketStatus { get; set; }

        public TicketTypeEnum TicketType { get; set; }        
    }       
}
