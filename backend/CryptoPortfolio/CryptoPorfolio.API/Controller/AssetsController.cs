
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    public class AssetsController : AbstractController
    {
        public AssetsController(ISender sender)
            : base(sender)
        {
        }

        [HttpGet("assets")]
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetAssets(CancellationToken cancellationToken)
            => await HandleRequest(new GetAssets(), cancellationToken);

        [HttpPost("asset")]
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsset([FromBody] AddAsset request, CancellationToken cancellationToken)
            => await HandleRequest(request, cancellationToken);

        [HttpPut("asset")]
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsset([FromBody] UpdateAsset request, CancellationToken cancellationToken)
            => await HandleRequest(request, cancellationToken);

        [HttpDelete("asset/{assetId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsset([FromRoute] int assetId, CancellationToken cancellationToken)
            => await HandleRequest(new DeleteAsset(assetId), cancellationToken);
    }
}
