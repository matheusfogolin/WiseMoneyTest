using WiseMoneyTest.Models;
using WiseMoneyTest.Repository;

namespace WiseMoneyTest.Services
{
    public class AccountService
    {
        public void CreateAccount(Account account)
        {
            AccountRepository accountRepository = new AccountRepository();
            accountRepository.CreateAccount(account);
        }

        public decimal GetBalance(Account account)
        {
            AccountRepository accountRepository = new AccountRepository();
            var balance = accountRepository.GetBalance(account);

            return balance;
        }

        public void Transfer(Account accountSending, Account accountReceiving, decimal value)
        {
            AccountRepository accountRepository = new AccountRepository();
            accountRepository.Transfer(accountSending, accountReceiving, value);
        }

        public List<Transactions> GetBankStatement(Account account, DateTime startingDate, DateTime finishDate)
        {
            AccountRepository accountRepository = new AccountRepository();
            var bankStatement = accountRepository.GetBankStatement(account, startingDate, finishDate);

            return bankStatement;
        }
    }
}
