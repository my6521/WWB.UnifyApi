namespace WWB.UnifyApi.Models
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ApiResult : ApiResult<object>
    {
    }

    public class ErrorApiResult : ApiResult
    {
        public object ErrorCode { get; set; }

        public ErrorApiResult()
        {
            IsSuccess = false;
        }

        public ErrorApiResult(object code, string msg) : this()
        {
            Message = msg;
            ErrorCode = code;
        }
    }
}