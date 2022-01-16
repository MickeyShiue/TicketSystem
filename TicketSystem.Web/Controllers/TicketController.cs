using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model.ResponseModel;
using TicketSystem.Web.Service.TicketService;

namespace TicketSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            this._ticketService = ticketService;
        }
       
        [Authorize(Roles = "QA,RD")]
        [HttpPost("TicketList")]
        public ActionResult<BaseResult<TicketListResponse>> GetTicketList()
        {
            var tickets = _ticketService.GetTickets(UserInfo);

            var ticketsResponse = new TicketListResponse() { Tickets = tickets };
            return new BaseResult<TicketListResponse>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), ticketsResponse);            
        }
    }
}
