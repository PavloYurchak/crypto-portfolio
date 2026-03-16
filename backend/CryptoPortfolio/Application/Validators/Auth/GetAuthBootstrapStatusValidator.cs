using CryptoPorfolio.Application.Requests.Auth;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Auth
{
    public sealed class GetAuthBootstrapStatusValidator : AbstractValidator<GetAuthBootstrapStatus>;
}
