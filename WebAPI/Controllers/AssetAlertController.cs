using Applications.UseCases.PMV.ServiceAlerts.Command;
using Applications.UseCases.PMV.ServiceAlerts.DTO;
using Applications.UseCases.PMV.ServiceAlerts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/pmv/[controller]")]
public class AssetAlertController : APIControllerBase
{
    private readonly IMediator _mediator;
    public AssetAlertController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index() {

        var query = new GetAlertsRequest();
        return HandleResult(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]GetAlertByIdRequest request) {
        return HandleResult(await _mediator.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> Post(ServiceAlertRequest request) {

        var cmd = new CreateUpdateAlertRequest(request);
        
        return HandleResult(await _mediator.Send(cmd));

    }

    

}