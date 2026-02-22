using System.Security.Claims;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    public class UserAssetTransactionsController : AbstractController
    {
        public UserAssetTransactionsController(ISender sender)
            : base(sender)
        {
        }

        [HttpGet("user-asset-transactions")]
        [ProducesResponseType(typeof(IEnumerable<UserAssetTransaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetUserAssetTransactions(
            [FromQuery] int assetId,
            [FromQuery] int currencyId,
            CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(new GetUserAssetTransactions(userId, assetId, currencyId), cancellationToken);
        }

        [HttpGet("user-asset-transactions/{transactionId:long}")]
        [ProducesResponseType(typeof(UserAssetTransaction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetUserAssetTransaction(
            [FromRoute] long transactionId,
            CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(new GetUserAssetTransaction(transactionId, userId), cancellationToken);
        }

        [HttpPost("user-asset-transaction")]
        [ProducesResponseType(typeof(UserAssetTransaction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AddUserAssetTransaction(
            [FromBody] AddUserAssetTransaction request,
            CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(request with { UserId = userId }, cancellationToken);
        }

        private bool TryGetUserId(out int userId)
        {
            userId = 0;
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdValue, out userId);
        }
    }
}
