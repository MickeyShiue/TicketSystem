using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        /// <summary>
        /// 取得所有 ticket
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "QA,RD")]
        [HttpGet("TicketList")]
        public ActionResult<BaseResult<TicketListResponse>> GetTicketList()
        {
            var tickets = _ticketService.GetTickets();

            var ticketsResponse = new TicketListResponse() { Tickets = tickets };
            return new BaseResult<TicketListResponse>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), ticketsResponse);
        }

        /// <summary>
        /// 取得單一 ticket 資訊
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "QA,RD")]
        [HttpPost("TicketById")]
        public ActionResult<BaseResult<TicketInfo>> GetTicketById(GetTicketByIdRequest getTicketByIdRequest)
        {
            var ticket = _ticketService.GetTicketById(getTicketByIdRequest.TicketId);          
            
            if (ticket == null)
                return new BaseResult<TicketInfo>((int)ApiResponseCodeEnum.BadRequest, ApiResponseCodeEnum.BadRequest.ToString(), null);

            return new BaseResult<TicketInfo>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), ticket);
        }

        /// <summary>
        /// 新增 ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [Authorize(Roles = "QA")]
        [HttpPost("CreateTicket")]
        public ActionResult<BaseResult<object>> CreateTicket(TicketInfo ticket)
        {
            var createSuccess = _ticketService.CreateTicket(ticket);
            if (createSuccess)
                return new BaseResult<object>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), null);

            return new BaseResult<object>((int)ApiResponseCodeEnum.InternalServerError, ApiResponseCodeEnum.InternalServerError.ToString(), null);
        }

        /// <summary>
        /// 修改 ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [Authorize(Roles = "QA,RD")]
        [HttpPost("UpdateTicket")]
        public ActionResult<BaseResult<object>> UpdateTicket(TicketInfo ticket)
        {
            var updateSuccess  = _ticketService.UpdateTicket(ticket,UserInfo.Role);         
            if (updateSuccess)
                return new BaseResult<object>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), null);

            return new BaseResult<object>((int)ApiResponseCodeEnum.InternalServerError, ApiResponseCodeEnum.InternalServerError.ToString(), null);           
        }

        /// <summary>
        /// 刪除 ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [Authorize(Roles = "QA")]
        [HttpPost("DeleteTicket")]
        public ActionResult<BaseResult<object>> DeleteTicket(TicketInfo ticket)
        {
            var deleteSuccess = _ticketService.DeleteTicket(ticket);
            if (deleteSuccess)
                return new BaseResult<object>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), null);

            return new BaseResult<object>((int)ApiResponseCodeEnum.InternalServerError, ApiResponseCodeEnum.InternalServerError.ToString(), null);
        }

        /// <summary>
        /// 根據角色取得相對應能夠使用的 ticketStatus
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "QA,RD")]
        [HttpPost("GetTicketStatus")]
        public ActionResult<BaseResult<TicketStatusResponse>> GetTicketStatus()
        {            
            var ticketStatusResponse = new TicketStatusResponse();
            ticketStatusResponse.TicketStatusOptions = _ticketService.GetTicketOptionsByRole(UserInfo.Role);

            if(ticketStatusResponse.TicketStatusOptions.Any())
                return new BaseResult<TicketStatusResponse>((int)ApiResponseCodeEnum.Success, ApiResponseCodeEnum.Success.ToString(), ticketStatusResponse);

            return new BaseResult<TicketStatusResponse>((int)ApiResponseCodeEnum.BadRequest, ApiResponseCodeEnum.BadRequest.ToString(), null);        
        }     
    }
}
