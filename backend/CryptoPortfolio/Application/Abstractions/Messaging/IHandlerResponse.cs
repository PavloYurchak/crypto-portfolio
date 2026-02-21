
namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    public interface IHandlerResponse<TResponse>
    {
        int Code { get; }

        TResponse Result { get; }

        string[]? Errors { get; }
    }
}
