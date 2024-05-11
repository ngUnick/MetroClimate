
using MetroClimate.Data.Constants;
using Newtonsoft.Json;

namespace MetroClimate.Data.Common;
public class ApiResult
{
    public ApiResult(ErrorCode code, string message)
    {
        ErrorCode = code.ToString();
        Success = false;
        Message = message;
    }

    public ApiResult(string message = "Completed")
    {
        Success = true;
        Message = message;
    }

    public bool Success { get; }
    public string Message { get; }
    public string ErrorCode { get; }
}

public class ApiResult<T> : ApiResult
{
    public ApiResult(T data, string message = "Completed") : base(message)
    {
        Data = data;
    }

    public ApiResult(T data, ErrorCode code, string message) : base(code, message)
    {
        Data = data;
    }

    public ApiResult(ErrorCode code, string message) : base(code, message) { }

    public T Data { get; }
}

