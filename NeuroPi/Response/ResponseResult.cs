using Microsoft.AspNetCore.Mvc;
using System.Net; // Import HttpStatusCode enum
using System.Threading.Tasks;

namespace NeuroPi.Response
{
    public class ResponseResult<T> : IActionResult
    {
        // HTTP Status Code for the response (changed to HttpStatusCode)
        public HttpStatusCode StatusCode { get; set; }

        // Message to accompany the response
        public string Message { get; set; }

        // Data to return in the response
        public T Data { get; set; }

        // Success response method - requires a custom HTTP status code (changed to HttpStatusCode)
        public static ResponseResult<T> SuccessResponse(HttpStatusCode statusCode, T data, string message = null)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message ?? "Request succeeded",
                Data = data
            };
        }

        // Failure response method - requires a custom HTTP status code (changed to HttpStatusCode)
        public static ResponseResult<T> FailResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = default(T)
            };
        }

        // Implement the ExecuteResultAsync method to conform to IActionResult
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode // Convert HttpStatusCode to int for the ObjectResult
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
