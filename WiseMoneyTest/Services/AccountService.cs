using System.Text.RegularExpressions;
using WiseMoneyTest.Entities;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Account;
using WiseMoneyTest.Repository;

namespace WiseMoneyTest.Services
{
    public class AccountService
    {
        private readonly AccountRepository accountRepository;

        public AccountService()
        {
            accountRepository = new AccountRepository();
        }

        public int GenerateRandomAccountNumber()
        {
            Random random = new Random();
            return random.Next(10000000);
        }
        public CreateAccountResponse CreateAccount(int userId)
        {
            int accountNumber = GenerateRandomAccountNumber();
            
            while (accountRepository.CheckIfAccountNumberAlreadyExists(accountNumber))
            {
                accountNumber = GenerateRandomAccountNumber();
            }

            var account = new Account(userId, accountNumber);

            accountRepository.CreateAccount(account);

            CreateAccountResponse createAccountResponse = new CreateAccountResponse(account.AccountNumber);

            return createAccountResponse;
        }
        public GetBalanceResponse GetBalance(int accountNumber, int userId)
        {
            var account = accountRepository.GetAccount(accountNumber, userId);
            if (account == null)
                throw new AccountNotFoundException("Conta não existe ou não pertence ao seu usuário.");

            GetBalanceResponse accountBalanceResponse = new GetBalanceResponse(account.Balance);
            
            return accountBalanceResponse;
        }
    }
}
