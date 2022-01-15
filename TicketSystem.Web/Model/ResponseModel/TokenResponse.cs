using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Web.Model.ResponseModel
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
