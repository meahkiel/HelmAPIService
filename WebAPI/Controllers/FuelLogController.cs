using Applications.UseCases.PMV.Fuels.Commands;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/pmv/[controller]")]
public class FuelLogController : APIControllerBase
{
    private readonly IMediator _mediator;

    public FuelLogController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("single")]
    public async Task<IActionResult> Single()
    {
        
        var cmd = new GetFuelLogQuery();
        return HandleResult(await _mediator.Send(cmd));
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FuelLogRequest request)
    {
        var cmd = new CreateLogCommand(request);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] FuelLogRequest request)
    {
        var cmd = new UpdateLogCommand(request);
        var result = await _mediator.Send(cmd);
        
        return HandleResult(result);
    }

    [HttpPost("/partial/{id}")]
    public async Task<IActionResult> PartialUpdate([FromBody] FuelLogRequest request)
    {
        var cmd = new UpdateLogCommand(request,true);
        var result = await _mediator.Send(cmd);
        
        return HandleResult(result);
    }



}