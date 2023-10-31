using System.Text.RegularExpressions;
using WiseMoneyTest.Entities;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Account;
using WiseMoneyTest.Repository;
using WiseMoneyTest.Repository.Interfaces;
using WiseMoneyTest.Services.Interfaces;

namespace WiseMoneyTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public AccountService(IAccountRepository repository, IHttpContextAccessor httpContextAcessor)
        {
            _accountRepository = repository;
            _httpContextAcessor = httpContextAcessor;
        }

        public int GenerateRandomAccountNumber()
        {
            Random random = new Random();
            return random.Next(10000000);
        }
        public CreateAccountResponse CreateAccount(Guid userId)
        {
            int accountNumber = GenerateRandomAccountNumber();
            
            while (_accountRepository.CheckIfAccountNumberAlreadyExists(accountNumber))
            {
                accountNumber = GenerateRandomAccountNumber();
            }

            var account = new Account(accountNumber, userId);

            _accountRepository.CreateAccount(account);

            CreateAccountResponse createAccountResponse = new CreateAccountResponse(account.AccountNumber);

            return createAccountResponse;
        }
        public GetBalanceResponse GetBalance(int accountNumber, Guid userId)
        {
            var account = _accountRepository.GetAccount(accountNumber, userId);
            if (account == null)
                throw new AccountNotFoundException("Conta não existe ou não pertence ao seu usuário.");

            GetBalanceResponse accountBalanceResponse = new GetBalanceResponse(account.Balance);
            
            return accountBalanceResponse;
        }

        public Guid FindUserFromRequest()
        {
            return Guid.Parse(_httpContextAcessor.HttpContext.User.FindFirst("Id").Value);
        }
    }
}
