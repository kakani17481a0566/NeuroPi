<<<<<<< Updated upstream
using Microsoft.AspNetCore.Mvc;
using System.Net;
=======
﻿using Microsoft.AspNetCore.Mvc;
using System.Net; // Import HttpStatusCode enum
>>>>>>> Stashed changes
using System.Threading.Tasks;

namespace NeuroPi.Response
{
    public class ResponseResult<T> : IActionResult
    {
<<<<<<< Updated upstream
        // HTTP Status Code for the response
=======
        // HTTP Status Code for the response (changed to HttpStatusCode)
>>>>>>> Stashed changes
        public HttpStatusCode StatusCode { get; set; }

        // Message to accompany the response
        public string Message { get; set; }

        // Data to return in the response
        public T Data { get; set; }

<<<<<<< Updated upstream
        // Success response method
=======
        // Success response method - requires a custom HTTP status code (changed to HttpStatusCode)
>>>>>>> Stashed changes
        public static ResponseResult<T> SuccessResponse(HttpStatusCode statusCode, T data, string message = null)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message ?? "Request succeeded",
                Data = data
            };
        }

<<<<<<< Updated upstream
        // Failure response method
=======
        // Failure response method - requires a custom HTTP status code (changed to HttpStatusCode)
>>>>>>> Stashed changes
        public static ResponseResult<T> FailResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseResult<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = default(T)
            };
        }

<<<<<<< Updated upstream
        // Executes the result within the context
=======
        // Implement the ExecuteResultAsync method to conform to IActionResult
>>>>>>> Stashed changes
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this)
            {
<<<<<<< Updated upstream
                StatusCode = (int)this.StatusCode
=======
                StatusCode = (int)this.StatusCode // Convert HttpStatusCode to int for the ObjectResult
>>>>>>> Stashed changes
            };

            await result.ExecuteResultAsync(context);
        }
    }
}