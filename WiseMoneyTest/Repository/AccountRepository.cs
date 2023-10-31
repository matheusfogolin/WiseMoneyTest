using WiseMoneyTest.Data;
using WiseMoneyTest.Entities;
using WiseMoneyTest.Repository.Interfaces;

namespace WiseMoneyTest.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WiseMoneyTestDbContext _context;
        public AccountRepository(WiseMoneyTestDbContext context)
        {
            _context = context;
        }
        public void CreateAccount(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
        }
        public bool CheckIfAccountNumberAlreadyExists(int accountNumber)
        {
            return _context.Accounts.Where(x => x.AccountNumber == accountNumber).Any();
        }
        public Account GetAccount(int accountNumber, Guid userId)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserId == userId && x.AccountNumber == accountNumber);
        }

        public Account GetAccount(int accountNumber)
        {
            return _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        }

        public void UpdateBalanceAfterTransaction(Account accountSending, Account accountReceiving, decimal transferValue)
        {
            accountSending.Balance -= transferValue;
            accountReceiving.Balance += transferValue;

            _context.SaveChanges();
        }

        public void UpdateBalanceAfterDeposit(Account account, decimal transferValue)
        {
            account.Balance += transferValue;

            _context.SaveChanges();
        }
    }
}
