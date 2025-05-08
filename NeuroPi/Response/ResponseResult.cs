using System.Net;

namespace NeuroPi.Response
{
    public class ResponseResult<T>
    {
        // Properties to hold the response data
        public HttpStatusCode Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        // Success response method - used for successful operations
        public static ResponseResult<T> SuccessResponse( HttpStatusCode code,T data, string message )
        {
            return new ResponseResult<T> { Success = code, Message = message, Data = data };
        }

        public static ResponseResult<T> FailResponse(HttpStatusCode code,string message)
        {
            return new ResponseResult<T> { Success =code, Message = message, Data = default(T) };
        }
    }
}
