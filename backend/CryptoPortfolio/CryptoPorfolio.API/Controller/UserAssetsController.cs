using System.Security.Claims;
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    public class UserAssetsController : AbstractController
    {
        public UserAssetsController(ISender sender)
            : base(sender)
        {
        }

        [HttpGet("user-assets")]
        [ProducesResponseType(typeof(IEnumerable<UserAsset>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetUserAssets(CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(new GetUserAssets(userId), cancellationToken);
        }

        [HttpPost("user-asset")]
        [ProducesResponseType(typeof(UserAsset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AddUserAsset([FromBody] AddUserAsset request, CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(request with { UserId = userId }, cancellationToken);
        }

        [HttpPut("user-asset")]
        [ProducesResponseType(typeof(UserAsset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsset([FromBody] UpdateUserAsset request, CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(request with { UserId = userId }, cancellationToken);
        }

        [HttpPost("user-asset/adjust")]
        [ProducesResponseType(typeof(UserAsset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AdjustUserAsset([FromBody] AdjustUserAsset request, CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(request with { UserId = userId }, cancellationToken);
        }

        [HttpDelete("user-asset/{assetId}/{currencyId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsset(
            [FromRoute] int assetId,
            [FromRoute] int currencyId,
            CancellationToken cancellationToken)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            return await HandleRequest(new DeleteUserAsset(userId, assetId, currencyId), cancellationToken);
        }

        private bool TryGetUserId(out int userId)
        {
            userId = 0;
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdValue, out userId);
        }
    }
}
