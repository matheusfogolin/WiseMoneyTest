using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Exceptions;
using WiseMoneyTest.Services;
using WiseMoneyTest.Services.Interfaces;

namespace WiseMoneyTest.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : WiseMoneyBaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult CreateAccount()
        {
            try
            {
                var userId = _accountService.FindUserFromRequest();
                var newAccount = _accountService.CreateAccount(userId);
                return Created("", newAccount);
            }
            catch
            {
                return ReturnDefaultError();
            }
        }
        [HttpGet ("{accountNumber}/balance")]
        public IActionResult GetBalance([FromRoute]int accountNumber)
        {
            try
            {
                var userId = _accountService.FindUserFromRequest();
                return Ok(_accountService.GetBalance(accountNumber, userId));
            }
            catch (Exception ex)
            {
                if (ex is AccountNotFoundException)
                    return NotFound(new ExceptionResponse(ex.Message));

                return ReturnDefaultError();
            }

            
        }

    }
}
