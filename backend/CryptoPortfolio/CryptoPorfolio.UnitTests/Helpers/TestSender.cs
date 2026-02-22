using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.UnitTests.Helpers
{
    public sealed class TestSender : ISender
    {
        private readonly Type _responseType;
        private readonly object _response;

        private TestSender(Type responseType, object response)
        {
            _responseType = responseType;
            _response = response;
        }

        public static TestSender ForResponse<TResponse>(HandlerResponse<TResponse> response)
            => new(typeof(TResponse), response);

        public Task<HandlerResponse<TResponse>> Send<TResponse>(
            IHandlerRequest<TResponse> request,
            CancellationToken ct = default)
        {
            if (_responseType != typeof(TResponse))
            {
                throw new InvalidOperationException(
                    $"TestSender expected response type {_responseType.Name} but got {typeof(TResponse).Name}.");
            }

            return Task.FromResult((HandlerResponse<TResponse>)_response);
        }
    }
}
