using Applications.UseCases.PMV.Assets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/pmv/[controller]")]
public class AssetController : APIControllerBase
{
    private readonly IMediator _mediator;

    public AssetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetAssetsRequest request) => HandleResult(await _mediator.Send(request));

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] ViewAssetRequest request) => HandleResult(await _mediator.Send(request));
}