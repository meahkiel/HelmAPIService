using Applications.UseCases.PMV.LogSheets;
using Applications.UseCases.PMV.LogSheets.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    [Route("api/pmv/[controller]")]
    public class LogSheetController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public LogSheetController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> SaveDraft(LogSheetOpenRequest request) {
            
            var cmd = new CreateLogSheetRequest(request);
            var result = await _mediator.Send(cmd);
            
            return HandleResult(result);
        }

        [HttpPost("close")]
        public async Task<IActionResult> Close(LogSheetCloseRequest request) {

            var cmd = new PostLogSheetRequest(request);
            var result = await _mediator.Send(cmd);
            
            return HandleResult(result);
        }


    }
}