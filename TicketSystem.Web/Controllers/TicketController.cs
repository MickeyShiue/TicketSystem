using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Web.Enum;
using TicketSystem.Web.Model;
using TicketSystem.Web.Model.RequestModel;
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

        [Authorize(Roles = "QA,RD")]
        [HttpPost("GetTicketById")]
        public ActionResult<BaseResult<TicketInfo>> GetTicketById(GetTicketByIdRequest getTicketByIdRequest)
        {
            var ticket = _ticketService.GetTicketById(getTicketByIdRequest.TicketId);
            ticket.TicketStatusOptions = _ticketService.GetTicketOptionsByRole(UserInfo.Role);
            ticket.Role = UserInfo.Role;

            if (ticket == null)
                return new BaseResult<TicketInfo>((int)ApiResponseCodeEnum.BadRequest, ApiResponseCodeEnum.BadRequest.ToString(), null);

            return new BaseResult<TicketInfo>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), ticket);
        }

        [Authorize(Roles = "QA,RD")]
        [HttpPost("UpdateTicket")]
        public ActionResult<BaseResult<object>> UpdateTicket(TicketInfo ticket)
        {
            var updateReuslt  = _ticketService.UpdateTicket(ticket);         
            if (updateReuslt)
                return new BaseResult<object>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), null);

            return new BaseResult<object>((int)ApiResponseCodeEnum.InternalServerError, ApiResponseCodeEnum.InternalServerError.ToString(), null);           
        }

        [Authorize(Roles = "QA")]
        [HttpPost("DeleteTicket")]
        public ActionResult<BaseResult<object>> DeleteTicket(TicketInfo ticket)
        {
            var updateReuslt = _ticketService.DeleteTicket(ticket);
            if (updateReuslt)
                return new BaseResult<object>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), null);

            return new BaseResult<object>((int)ApiResponseCodeEnum.InternalServerError, ApiResponseCodeEnum.InternalServerError.ToString(), null);
        }
    }
}
