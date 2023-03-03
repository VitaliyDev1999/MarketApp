using MarketApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowOrigin")]
public class MarketDataController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMarketData(
        [FromServices] IMediator _mediator)
    {
        var result = await _mediator.Send(new GetMarketDataQuery());

        if (result == null || !result.Any())
            return NotFound();

        return Ok(result);
    }
}
