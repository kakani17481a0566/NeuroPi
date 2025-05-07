using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace NeuroPi.Response
{
    public class ResponseResult<T> : IActionResult
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        public ResponseResult(int status = 200, string message = "Success", T data = default)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new JsonResult(new
            {
                status = Status,
                message = Message,
                data = Data
            })
            {
                StatusCode = Status
            };

            await result.ExecuteResultAsync(context);
        }

        // ✅ Static Helpers for cleaner usage
        public static ResponseResult<T> Success(T data, string message = "Success")
            => new ResponseResult<T>(200, message, data);

        public static ResponseResult<T> Error(string message, int statusCode = 500)
            => new ResponseResult<T>(statusCode, message, default);
    }
}
