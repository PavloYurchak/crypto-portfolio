using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Auth;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        [Test]
        public async Task Register_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<AuthResult>.Ok(TestModels.AuthResult()));
            var controller = new AuthController(sender);
            var request = new RegisterUser("user@test.local", "user", "password");

            var result = await controller.Register(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task Login_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<AuthResult>.Ok(TestModels.AuthResult()));
            var controller = new AuthController(sender);
            var request = new LoginUser("user@test.local", "password");

            var result = await controller.Login(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task Refresh_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<AuthResult>.Ok(TestModels.AuthResult()));
            var controller = new AuthController(sender);
            var request = new RefreshUserToken("refresh");

            var result = await controller.Refresh(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
