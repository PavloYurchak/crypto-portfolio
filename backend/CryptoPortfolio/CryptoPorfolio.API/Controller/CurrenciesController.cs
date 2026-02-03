// <copyright file="CurrenciesController.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Currenices;
using CryptoPorfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    public class CurrenciesController : AbstractController
    {
        public CurrenciesController(ISender sender)
            : base(sender)
        {
        }

        [HttpGet("currencies")]
        [ProducesResponseType(typeof(Currency), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetCurrencies(CancellationToken cancellationToken)
        {
            return await this.HandleRequest(new GetCurrencies(), cancellationToken);
        }

        [HttpPost("currency")]
        [ProducesResponseType(typeof(Currency), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCurrency([FromBody] AddCurrency request, CancellationToken cancellationToken)
        {
            return await this.HandleRequest(request, cancellationToken);
        }

        [HttpPut("currency")]
        [ProducesResponseType(typeof(Currency), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrency request, CancellationToken cancellationToken)
        {
            return await this.HandleRequest(request, cancellationToken);
        }

        [HttpDelete("currency/{currencyId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCurrency([FromRoute] int currencyId, CancellationToken cancellationToken)
        {
            return await this.HandleRequest(new DeleteCurrency(currencyId), cancellationToken);
        }
    }
}
