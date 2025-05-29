    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    namespace NeuroPi.UserManagment.Response
    {
        public class ResponseResult<T> : IActionResult
        {
        private HttpStatusCode badRequest;
        private global::SchoolManagement.ViewModel.Master.MasterResponseVM? response;
        private string v;
        private HttpStatusCode oK;
        private global::SchoolManagement.ViewModel.Master.MasterResponseVM response1;

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

        public ResponseResult(HttpStatusCode badRequest, global::SchoolManagement.ViewModel.Master.MasterResponseVM? response, string v)
        {
            this.badRequest = badRequest;
            this.response = response;
            this.v = v;
        }

        public ResponseResult(HttpStatusCode oK, global::SchoolManagement.ViewModel.Master.MasterResponseVM response1, string v)
        {
            this.oK = oK;
            this.response1 = response1;
            this.v = v;
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
