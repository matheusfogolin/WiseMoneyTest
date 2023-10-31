using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Exceptions;
using WiseMoneyTest.Models.Transactions;
using WiseMoneyTest.Repository.Interfaces;
using WiseMoneyTest.Services;
using WiseMoneyTest.Services.Interfaces;

namespace WiseMoneyTest.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    [Authorize]
    public class TransactionController : WiseMoneyBaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(ITransactionService transactionService, IAccountService accountService, IAccountRepository accountRepository)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        [HttpPost("transfer")]
        public IActionResult Transfer(TransferInputModel transferInputModel)
        {
            try
            {
                var userId = _accountService.FindUserFromRequest();
                _transactionService.Transfer(transferInputModel, userId);
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
                _transactionService.Deposit(depositInputModel);
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
                var statements = _transactionService.GetBankStatement(accountNumber, startingDate, finishDate);
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
