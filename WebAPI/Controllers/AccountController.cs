using Applications.Contracts.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : APIControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Login([FromBody]LoginQuery query) {

        return HandleResult(await _mediator.Send(query));
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Profile([FromQuery]string empCode){
        
        return HandleResult(await _mediator.Send(new UserProfileQuery(empCode)));
    }
}