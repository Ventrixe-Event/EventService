/*
 * Event Result - Response Wrapper Pattern (EventResult.cs)
 * ========================================================
 *
 * This generic class implements the Result pattern for consistent API responses
 * throughout the Event Service. It wraps operation results with success/failure
 * information and provides standardized error handling.
 *
 * Result Pattern Features:
 * - Consistent success/failure indication across all API operations
 * - Type-safe data payload for successful operations
 * - Error message handling for failed operations
 * - Optional informational messages for additional context
 * - JSON property naming for frontend compatibility
 *
 * Benefits:
 * - Eliminates exception-based error handling for business logic
 * - Provides consistent API response structure
 * - Enables better error handling in frontend applications
 * - Simplifies unit testing of business operations
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for result pattern design,
 *   factory method implementation, and JSON serialization configuration
 * - Uses JSON property name attributes for camelCase serialization
 * - Follows functional programming principles for error handling
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

using System.Text.Json.Serialization;

namespace Application.Models;

/// <summary>
/// Generic result wrapper for API operations implementing the Result pattern.
/// Provides consistent structure for success/failure responses with optional data payload.
/// </summary>
/// <typeparam name="T">The type of data returned on successful operations</typeparam>
public class EventResult<T>
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// True for successful operations, false for failures.
    /// Serialized as "success" in JSON for frontend compatibility.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// The data payload returned on successful operations.
    /// Contains the actual result data (e.g., event object, list of events).
    /// Null for failed operations. Serialized as "data" in JSON.
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    /// <summary>
    /// Error message describing what went wrong in failed operations.
    /// Null for successful operations. Serialized as "error" in JSON.
    /// Should provide meaningful information for debugging and user feedback.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Optional informational message providing additional context.
    /// Can be used for both successful and failed operations.
    /// Serialized as "message" in JSON.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Factory method for creating successful result instances.
    /// Simplifies creation of success responses with data payload.
    /// </summary>
    /// <param name="data">The data to include in the successful result</param>
    /// <param name="message">Optional success message for additional context</param>
    /// <returns>A new EventResult instance indicating success with the provided data</returns>
    public static EventResult<T> SuccessResult(T data, string? message = null)
    {
        return new EventResult<T>
        {
            Success = true,
            Data = data,
            Message = message,
        };
    }

    /// <summary>
    /// Factory method for creating failure result instances.
    /// Simplifies creation of error responses with descriptive error messages.
    /// </summary>
    /// <param name="error">The error message describing what went wrong</param>
    /// <returns>A new EventResult instance indicating failure with the provided error message</returns>
    public static EventResult<T> FailureResult(string error)
    {
        return new EventResult<T> { Success = false, Error = error };
    }
}
