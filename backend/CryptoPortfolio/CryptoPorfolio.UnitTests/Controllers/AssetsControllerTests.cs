using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class AssetsControllerTests
    {
        [Test]
        public async Task GetAssets_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<IEnumerable<Asset>>.Ok(new[] { TestModels.Asset() }));

            var controller = ControllerFactory.CreateWithUser(new AssetsController(sender));

            var result = await controller.GetAssets(CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task AddAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<Asset>.Ok(TestModels.Asset()));
            var controller = ControllerFactory.CreateWithUser(new AssetsController(sender));
            var request = new AddAsset { Name = "Bitcoin", Symbol = "BTC" };

            var result = await controller.AddAsset(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task UpdateAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<Asset>.Ok(TestModels.Asset()));
            var controller = ControllerFactory.CreateWithUser(new AssetsController(sender));
            var request = new UpdateAsset(1) { Name = "Bitcoin", Symbol = "BTC" };

            var result = await controller.UpdateAsset(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task DeleteAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<bool>.Ok(true));
            var controller = ControllerFactory.CreateWithUser(new AssetsController(sender));

            var result = await controller.DeleteAsset(1, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
