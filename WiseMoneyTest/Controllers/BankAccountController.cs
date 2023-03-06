using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Exceptions;
using WiseMoneyTest.Services;

namespace WiseMoneyTest.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class BankAccountController : WiseMoneyBaseController
    {
        private readonly AccountService accountService;

        public BankAccountController()
        {
            accountService = new AccountService();
        }

        [HttpPost]
        public IActionResult CreateAccount()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var newAccount = accountService.CreateAccount(userId);
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
                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                return Ok(accountService.GetBalance(accountNumber, userId));
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
