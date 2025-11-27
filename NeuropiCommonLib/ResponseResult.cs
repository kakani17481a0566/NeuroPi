    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    namespace NeuroPi.CommonLib.Model
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
                StatusCode = (int)StatusCode
            };

            // Asynchronously execute the result
            await result.ExecuteResultAsync(context);
        }
    }
}
