// <copyright file="ApiStatusController.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.TestAPI;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiStatusController(ISender sender)
        : AbstractController(sender)
    {
        [HttpGet("status")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStatus(CancellationToken cancellationToken)
            => await this.HandleRequest(new ApiStatus(), cancellationToken);
    }
}
