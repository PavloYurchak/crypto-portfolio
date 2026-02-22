using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.UnitTests.Helpers
{
    public static class TestAssertions
    {
        public static void AssertOk(IActionResult result)
        {
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok!.StatusCode ?? StatusCodes.Status200OK, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}
