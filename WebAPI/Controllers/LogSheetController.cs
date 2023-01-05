using Applications.UseCases.PMV.LogSheets;
using Applications.UseCases.PMV.LogSheets.Commands;
using Applications.UseCases.PMV.LogSheets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/pmv/[controller]")]
public class LogSheetController : APIControllerBase
{
    private readonly IMediator _mediator;

    public LogSheetController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetLogSheetDraftsQuery query)
    {
        return HandleResult(await _mediator.Send(query));
    }

    [HttpGet("transaction-report")]
    public async Task<IActionResult> GetTransactions([FromQuery]string dateFrom , string dateTo) {

        var query = new GetLogSheetTransactionsQuery(dateFrom,dateTo);
        
        return HandleResult(await _mediator.Send(query));
    }



    [HttpPost]
    public async Task<IActionResult> SaveDraft(LogSheetOpenRequest request)
    {

        var cmd = new CreateLogSheetCommand(request);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }

    [HttpPost("close")]
    public async Task<IActionResult> Close(LogSheetCloseRequest request)
    {

        var cmd = new PostLogSheetRequest(request);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }

    [HttpPost("detail")]
    public async Task<IActionResult> Detail(LogSheetDetailRequest detail)
    {
        var cmd = new UpsertLogDetailCommand(detail);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }

    [HttpPost("detail/update")]
    public async Task<IActionResult> Update(LogSheetDetailRequest detail)
    {
        var cmd = new UpsertLogDetailCommand(detail);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }


    [HttpPost("full")]
    public async Task<IActionResult> CreateFull([FromBody] LogSheetRequest request)
    {

        var cmd = new UpsertLogSheetCommand(request);
        var result = await _mediator.Send(cmd);

        return HandleResult(result);
    }




}