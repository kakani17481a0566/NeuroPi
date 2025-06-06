using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SchoolManagement.Response
{
    public class ResponseResult<T> : IActionResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ResponseResult(HttpStatusCode statusCode, T data, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? "Request completed"; // Default message if none is provided
            Data = data;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this)
            {
                StatusCode = (int)StatusCode
            };
            await result.ExecuteResultAsync(context);
        }
    }
}
