// <copyright file="TestController.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    public class TestController(ISender sender)
        : AbstractController(sender)
    {
        //[HttpGet("test")]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //public async Task<IActionResult> TestEndpoint(CancellationToken cancellationToken)
        //    => await this.HandleRequest(new TestRequest(), cancellationToken);
    }
}
