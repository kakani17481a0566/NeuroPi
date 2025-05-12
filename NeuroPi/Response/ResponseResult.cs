using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace NeuroPi.Response
{
    public class ResponseResult<T> : IActionResult
    {
        // HTTP status code
        public HttpStatusCode StatusCode { get; set; }

        // Message to accompany the response
        public string Message { get; set; }

        // Data to return in the response
        public T Data { get; set; }

        // Constructor to initialize ResponseResult with all properties
        public ResponseResult(HttpStatusCode statusCode, T data, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? "Request completed"; // Default message if none is provided
            Data = data;
        }

        // Required method for IActionResult implementation
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

            // Asynchronously execute the result
            await result.ExecuteResultAsync(context);
        }
    }
}
