using System.Text.Json.Serialization;

namespace Application.Models;

public class EventResult<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("message")]
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
