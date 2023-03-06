using Microsoft.AspNetCore.Mvc;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Exceptions;
using WiseMoneyTest.Models.User;
using WiseMoneyTest.Services;

namespace WiseMoneyTest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : WiseMoneyBaseController
    {
        private readonly UserService userService;
        public UserController()
        {
            userService = new UserService();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginInputModel loginInputModel)
        {
            try
            {
                var authenticate = userService.Authenticate(loginInputModel);
                return Ok(authenticate);
            }
            catch (Exception ex)
            {
                if (ex is AccountNotFoundException)
                    return NotFound(new ExceptionResponse(ex.Message));

                return ReturnDefaultError();
            }
        }

        [HttpPost("create")]
        public IActionResult CreateUser(CreateUserInputModel createUserInputModel)
        {
            try
            {
                userService.CreateUser(createUserInputModel);
                return Created("", createUserInputModel);
            }
            catch(Exception ex)
            {
                if (ex is InvalidEmailException)
                    return BadRequest(new ExceptionResponse(ex.Message));

                return ReturnDefaultError();
            }
        }
    }
}
