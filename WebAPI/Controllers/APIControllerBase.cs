using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    public class APIControllerBase : ControllerBase
    {
        

        public IActionResult HandleResult<T>(Result<T> result) {

            if(result == null) {
                return Problem(title: "Not Found",statusCode: StatusCodes.Status404NotFound);
            }
            else {
                if(!result.IsSuccess) {
                    var resultError = result.Errors[0];
                    return Problem(title: resultError.Message, statusCode: StatusCodes.Status400BadRequest);
                }

                return Ok(result.Value);
            }
        }
    }
}