using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TicketSystem.Web.Enums;

namespace TicketSystem.Web.Model
{
    public class UserInfo
    {
        public string UserName { get; set; }

        [JsonIgnore]
        public string PassWord { get; set; }

        public RoleEnum Role { get; set; }

        public IEnumerable<TicketTypeEnum> TicketTypes { get; set; }

        public IEnumerable<TicketStatusEnum> TicketStatuses { get; set; }

    }
}
