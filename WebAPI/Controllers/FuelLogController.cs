using Applications.UseCases.PMV.Fuels.Commands;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


/***



*/


[Route("api/pmv/[controller]")]
public class FuelLogController : APIControllerBase
{
    private readonly IMediator _mediator;

    public FuelLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("new")]
    public async Task<IActionResult> New([FromQuery]string? station,bool IsPostBack)
    {
        var cmd = new GetFuelLogQuery(null,station,IsPostBack);
        return HandleResult(await _mediator.Send(cmd));
    }


    [HttpGet("single")]
    public async Task<IActionResult> Single([FromQuery]string? Id,bool IsPostBack)
    {
        
        var cmd = new GetFuelLogQuery(Id,null,IsPostBack);
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
        
        return Ok(request);
    }

    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromBody] FuelLogCloseRequest request)
    {
        var cmd = new SubmitLogCommand(request);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }

    [HttpGet("draft")]
    public async Task<IActionResult> GetDraftLogs([FromQuery] GetDraftLogQuery query) {

        return HandleResult(await _mediator.Send(query));
        
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetFuelLogs([FromQuery] GetFuelLogTransactionQuery query) {

        return HandleResult(await _mediator.Send(query));

    }

    [HttpGet("efficiency")]
    public async Task<IActionResult> GetFuelEfficiency([FromQuery] GetFuelEfficiencyQuery query) {
        return HandleResult(await _mediator.Send(query));
    }
}