
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
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var result = await sender.Send(request, cancellationToken);

                return ToActionResult(result);
            }
            catch (OperationCanceledException)
            {
                return Problem(title: "Request canceled", statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return Problem(
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
                201 => StatusCode(StatusCodes.Status201Created),
                204 => NoContent(),

                400 => BadRequest(result.Error),
                401 => Unauthorized(result.Error),
                403 => Forbid(),
                404 => NotFound(result.Error),
                409 => Conflict(result.Error),

                int c when c >= 500 => Problem(title: "Error", detail: result.Error, statusCode: c),
                503 => StatusCode(StatusCodes.Status503ServiceUnavailable, result.Error),

                _ => Ok(result),
            };
        }
    }
}
