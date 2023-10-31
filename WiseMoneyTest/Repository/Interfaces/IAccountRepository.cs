using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Repository.Interfaces
{
    public interface IAccountRepository
    {
        public void CreateAccount(Account account);
        public bool CheckIfAccountNumberAlreadyExists(int accountNumber);
        public Account GetAccount(int accountNumber, Guid userId);
        public Account GetAccount(int accountNumber);
        public void UpdateBalanceAfterTransaction(Account accountSending, Account accountReceiving, decimal transferValue);
        public void UpdateBalanceAfterDeposit(Account account, decimal transferValue);

    }
}
