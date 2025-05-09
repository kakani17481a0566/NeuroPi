
﻿using Microsoft.AspNetCore.Mvc;
using System.Net;

using System.Threading.Tasks;

namespace NeuroPi.Response
{
    public class ResponseResult<T> : IActionResult
    {

        public HttpStatusCode StatusCode { get; set; }

        // Message to accompany the response
        public string Message { get; set; }

        // Data to return in the response
        public T Data { get; set; }


        public static ResponseResult<T> SuccessResponse(HttpStatusCode statusCode, T data, string message = null)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message ?? "Request succeeded",
                Data = data
            };
        }

        public static ResponseResult<T> FailResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = default(T)
            };
        }

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