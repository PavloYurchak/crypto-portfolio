using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class ApiStatusControllerTests
    {
        [Test]
        public async Task GetStatus_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<bool>.Ok(true));
            var controller = ControllerFactory.CreateWithUser(new ApiStatusController(sender));

            var result = await controller.GetStatus(CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
