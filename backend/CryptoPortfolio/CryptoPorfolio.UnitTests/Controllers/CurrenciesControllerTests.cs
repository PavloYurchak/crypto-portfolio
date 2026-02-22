using CryptoPorfolio.API.Controller;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Currenices;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.UnitTests.Helpers;

namespace CryptoPorfolio.UnitTests.Controllers
{
    public class CurrenciesControllerTests
    {
        [Test]
        public async Task GetCurrencies_ReturnsOk()
        {
            var sender = TestSender.ForResponse(
                HandlerResponse<IEnumerable<Currency>>.Ok(new[] { TestModels.Currency() }));

            var controller = ControllerFactory.CreateWithUser(new CurrenciesController(sender));

            var result = await controller.GetCurrencies(CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task AddCurrency_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<Currency>.Ok(TestModels.Currency()));
            var controller = ControllerFactory.CreateWithUser(new CurrenciesController(sender));
            var request = new AddCurrency { Name = "US Dollar", Symbol = "USD" };

            var result = await controller.AddCurrency(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task UpdateCurrency_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<Currency>.Ok(TestModels.Currency()));
            var controller = ControllerFactory.CreateWithUser(new CurrenciesController(sender));
            var request = new UpdateCurrency(1) { Name = "US Dollar", Symbol = "USD" };

            var result = await controller.UpdateCurrency(request, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }

        [Test]
        public async Task DeleteCurrency_ReturnsOk()
        {
            var sender = TestSender.ForResponse(HandlerResponse<bool>.Ok(true));
            var controller = ControllerFactory.CreateWithUser(new CurrenciesController(sender));

            var result = await controller.DeleteCurrency(1, CancellationToken.None);

            TestAssertions.AssertOk(result);
        }
    }
}
