using Microsoft.AspNetCore.Mvc;
using WiseMoneyTest.Models.Exceptions;

namespace WiseMoneyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WiseMoneyBaseController : ControllerBase
    {
        protected IActionResult ReturnDefaultError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionResponse("Serviço indisponível no momento."));
        }
    }
}
