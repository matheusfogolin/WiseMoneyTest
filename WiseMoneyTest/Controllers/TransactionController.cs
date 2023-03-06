using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Exceptions;
using WiseMoneyTest.Models.Transactions;
using WiseMoneyTest.Services;

namespace WiseMoneyTest.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    [Authorize]
    public class TransactionController : WiseMoneyBaseController
    {
        private readonly TransactionService transactionService;

        public TransactionController()
        {
            transactionService = new TransactionService();
        }
        [HttpPost("transfer")]
        public IActionResult Transfer(TransferInputModel transferInputModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                transactionService.Transfer(transferInputModel, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is AccountValidationException)
                    return BadRequest(new ExceptionResponse(ex.Message));

                return ReturnDefaultError();
            }
        }
        [HttpPost("deposit")]
        public IActionResult Deposit(DepositInputModel depositInputModel)
        {
            try
            {
                transactionService.Deposit(depositInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is AccountNotFoundException)
                    return NotFound(new ExceptionResponse(ex.Message));

                return ReturnDefaultError();
            }
        }

        [HttpGet("{accountNumber}/bankstatement")]
        public IActionResult GetBankStatement([FromRoute]int accountNumber, DateTime startingDate, DateTime finishDate)
        {
            try
            {
                var statements = transactionService.GetBankStatement(accountNumber, startingDate, finishDate);
                return Ok(statements);
            }
            catch(Exception ex)
            {
                if (ex is AccountValidationException)
                    return BadRequest(new ExceptionResponse(ex.Message));

                return ReturnDefaultError();
            }
        }
    }
}
