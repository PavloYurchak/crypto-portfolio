
namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    public sealed class HandlerResponse<T>
    {
        public bool Success { get; init; }

        public string? Error { get; init; }

        public int? StatusCode { get; init; }

        public T? Result { get; init; }

        public static HandlerResponse<T> Ok(T result) => new() { Success = true, Result = result, StatusCode = 200 };

        public static HandlerResponse<T> BadRequest(string error) => new() { Success = false, Error = error, StatusCode = 400 };

        public static HandlerResponse<T> NotFound(string error) => new() { Success = false, Error = error, StatusCode = 404 };

        public static HandlerResponse<T> Fail(string error, int? code = 500) => new() { Success = false, Error = error, StatusCode = code ?? 500 };
    }
}
