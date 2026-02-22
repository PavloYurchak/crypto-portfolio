using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class UserAssetTransactionsControllerTests
    {
        [Test]
        public async Task GetUserAssetTransactions_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<IEnumerable<UserAssetTransaction>>.Ok(new[] { TestModels.UserAssetTransaction() }));

            var controller = ControllerFactory.CreateWithUser(new UserAssetTransactionsController(sender));

            var result = await controller.GetUserAssetTransactions(1, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task GetUserAssetTransaction_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<UserAssetTransaction>.Ok(TestModels.UserAssetTransaction()));

            var controller = ControllerFactory.CreateWithUser(new UserAssetTransactionsController(sender));

            var result = await controller.GetUserAssetTransaction(1, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task AddUserAssetTransaction_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<UserAssetTransaction>.Ok(TestModels.UserAssetTransaction()));

            var controller = ControllerFactory.CreateWithUser(new UserAssetTransactionsController(sender));
            var request = new AddUserAssetTransaction
            {
                AssetId = 1,
                CurrencyId = 1,
                TransactionTypeCode = "BUY",
                Quantity = 1.5m,
                Amount = 30000m,
                Price = 20000m,
                ExecutedAt = DateTime.UtcNow,
            };

            var result = await controller.AddUserAssetTransaction(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
