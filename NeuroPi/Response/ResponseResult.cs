namespace NeuroPi.Response
{
    public class ResponseResult<T>
    {
        // Properties to hold the response data
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        // Success response method - used for successful operations
        public static ResponseResult<T> SuccessResponse(T data, string message = null)
        {
            return new ResponseResult<T> { Success = true, Message = message, Data = data };
        }

        public static ResponseResult<T> FailResponse(string message)
        {
            return new ResponseResult<T> { Success = false, Message = message, Data = default(T) };
        }
    }
}
