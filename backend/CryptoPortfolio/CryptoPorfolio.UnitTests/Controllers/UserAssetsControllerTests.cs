using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class UserAssetsControllerTests
    {
        [Test]
        public async Task GetUserAssets_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<IEnumerable<UserAsset>>.Ok(new[] { TestModels.UserAsset() }));

            var controller = ControllerFactory.CreateWithUser(new UserAssetsController(sender));

            var result = await controller.GetUserAssets(CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task AddUserAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<UserAsset>.Ok(TestModels.UserAsset()));
            var controller = ControllerFactory.CreateWithUser(new UserAssetsController(sender));
            var request = new AddUserAsset
            {
                AssetId = 1,
                CurrencyId = 1,
                Quantity = 1.5m,
                Price = 20000m,
            };

            var result = await controller.AddUserAsset(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task UpdateUserAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<UserAsset>.Ok(TestModels.UserAsset()));
            var controller = ControllerFactory.CreateWithUser(new UserAssetsController(sender));
            var request = new UpdateUserAsset
            {
                AssetId = 1,
                CurrencyId = 1,
                Quantity = 2.0m,
                Price = 21000m,
            };

            var result = await controller.UpdateUserAsset(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task AdjustUserAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<UserAsset>.Ok(TestModels.UserAsset()));
            var controller = ControllerFactory.CreateWithUser(new UserAssetsController(sender));
            var request = new AdjustUserAsset
            {
                AssetId = 1,
                CurrencyId = 1,
                DeltaQuantity = 0.5m,
                Price = 20000m,
            };

            var result = await controller.AdjustUserAsset(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task DeleteUserAsset_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<bool>.Ok(true));
            var controller = ControllerFactory.CreateWithUser(new UserAssetsController(sender));

            var result = await controller.DeleteUserAsset(1, 1, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
