
namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    public interface ISender
    {
        Task<HandlerResponse<TResponse>> Send<TResponse>(
            IHandlerRequest<TResponse> request,
            CancellationToken ct = default);
    }
}
