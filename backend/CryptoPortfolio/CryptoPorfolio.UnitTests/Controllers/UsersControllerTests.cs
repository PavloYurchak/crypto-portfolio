using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Test]
        public async Task GetCurrentUser_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<UserResponse>.Ok(TestModels.UserResponse()));
            var controller = ControllerFactory.CreateWithUser(new UsersController(sender));

            var result = await controller.GetCurrentUser(CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task GetUsers_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<IEnumerable<UserResponse>>.Ok(new[] { TestModels.UserResponse() }));

            var controller = ControllerFactory.CreateWithUser(new UsersController(sender));

            var result = await controller.GetUsers(CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task BlockUser_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<UserResponse>.Ok(TestModels.UserResponse()));
            var controller = ControllerFactory.CreateWithUser(new UsersController(sender));

            var result = await controller.BlockUser(1, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
