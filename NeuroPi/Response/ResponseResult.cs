using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace NeuroPi.Response
{
    public class ResponseResult<T> : IActionResult
    {
        // HTTP Status Code for the response
        public HttpStatusCode StatusCode { get; set; }

        // Message to accompany the response
        public string Message { get; set; }

        // Data to return in the response
        public T Data { get; set; }

        // Success response method
        public static ResponseResult<T> SuccessResponse(HttpStatusCode statusCode, T data, string message = null)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message ?? "Request succeeded",
                Data = data
            };
        }

        // Failure response method
        public static ResponseResult<T> FailResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = default(T)
            };
        }

        // Executes the result within the context
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
