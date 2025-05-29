namespace Application.Models;

public class EventResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }
    public string? Message { get; set; }

    public static EventResult<T> SuccessResult(T data, string? message = null)
    {
        return new EventResult<T>
        {
            Success = true,
            Data = data,
            Message = message,
        };
    }

    public static EventResult<T> FailureResult(string error)
    {
        return new EventResult<T> { Success = false, Error = error };
    }
}
