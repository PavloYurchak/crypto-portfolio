// <copyright file="AuthController.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Auth;
using CryptoPorfolio.Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ISender sender) : AbstractController(sender)
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUser request, CancellationToken cancellationToken)
            => await this.HandleRequest(request, cancellationToken);

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUser request, CancellationToken cancellationToken)
            => await this.HandleRequest(request, cancellationToken);

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshUserToken request, CancellationToken cancellationToken)
            => await this.HandleRequest(request, cancellationToken);
    }
}
