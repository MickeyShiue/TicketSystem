namespace TicketSystem.Web.Model.ResponseModel
{
    public class BaseResult<TEntity>
    {
        public int ErrorCode { get; }

        public string Message { get; }

        public TEntity Data { get; }

        public BaseResult(int errorCode, string message, TEntity data)
        {         
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Data = data;
        }
    }
}
