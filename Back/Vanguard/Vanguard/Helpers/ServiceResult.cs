namespace Vanguard.Helpers
{
    public class ServiceResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public int StatusCode { get; private set; }

        public ServiceResult(bool success, string message, int statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }

        public static ServiceResult Ok(string message) => new ServiceResult(true, message, 200);
        public static ServiceResult NotFound(string message) => new ServiceResult(false, message, 404);
        public static ServiceResult BadRequest(string message) => new ServiceResult(false, message, 400);
        public static ServiceResult InternalServerError(string message) => new ServiceResult(false, message, 500);
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; private set; }

        public ServiceResult(bool success, string message, int statusCode, T data)
            : base(success, message, statusCode)
        {
            Data = data;
        }

        public static ServiceResult<T> Ok(string message, T data) => new ServiceResult<T>(true, message, 200, data);
        public static new ServiceResult<T> NotFound(string message) => new ServiceResult<T>(false, message, 404, default(T));
        public static new ServiceResult<T> BadRequest(string message) => new ServiceResult<T>(false, message, 400, default(T));
        public static new ServiceResult<T> InternalServerError(string message) => new ServiceResult<T>(false, message, 500, default(T));
    }
}
