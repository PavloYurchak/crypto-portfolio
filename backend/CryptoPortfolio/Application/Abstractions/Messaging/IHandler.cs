
namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    public interface IHandler<in TRequest, TResponse>
    where TRequest : IHandlerRequest<TResponse>
    {
        Task<HandlerResponse<TResponse>> Handle(TRequest request, CancellationToken ct);
    }
}
