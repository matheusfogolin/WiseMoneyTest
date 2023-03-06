using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Repository
{
    public class AccountRepository
    {
        public static List<Account> AccountList = new List<Account>();

        
        public void CreateAccount(Account account)
        {
            int lastId;

            if (AccountList.Count > 0)
            {
                lastId = AccountList.Max(x => x.AccountId);
            }
            else
            {
                lastId = 0;
            }

            account.AccountId = lastId + 1;

            AccountList.Add(account);
        }
        public bool CheckIfAccountNumberAlreadyExists(int accountNumber)
        {
            return AccountList.Where(x => x.AccountNumber == accountNumber).Any();
        }

        public Account GetAccount(int accountNumber, int userId)
        {
            var account = AccountList.FirstOrDefault(x => x.UserId == userId && x.AccountNumber == accountNumber);

            return account;
        }

        public Account GetAccount(int accountNumber)
        {
            var account = AccountList.FirstOrDefault(x => x.AccountNumber == accountNumber);

            return account;
        }
    }
}
