// <copyright file="AbstractController.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPorfolio.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController(ISender sender) : ControllerBase
    {
        protected async Task<IActionResult> HandleRequest<TResponse>(
            IHandlerRequest<TResponse> request,
            CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem(this.ModelState);
            }

            try
            {
                var result = await sender.Send(request, cancellationToken);

                return this.ToActionResult(result);
            }
            catch (OperationCanceledException)
            {
                return this.Problem(title: "Request canceled", statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return this.Problem(
                    title: "Unexpected error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        private IActionResult ToActionResult<TResponse>(HandlerResponse<TResponse> result)
        {
            var code = result.StatusCode ?? (result.Success ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError);

            return code switch
            {
                201 => this.StatusCode(StatusCodes.Status201Created),
                204 => this.NoContent(),

                400 => this.BadRequest(result.Error),
                401 => this.Unauthorized(result.Error),
                403 => this.Forbid(),
                404 => this.NotFound(result.Error),
                409 => this.Conflict(result.Error),

                int c when c >= 500 => this.Problem(title: "Error", detail: result.Error, statusCode: c),
                503 => this.StatusCode(StatusCodes.Status503ServiceUnavailable, result.Error),

                _ => this.Ok(result),
            };
        }
    }
}
