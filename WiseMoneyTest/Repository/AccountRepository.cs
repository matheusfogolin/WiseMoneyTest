using WiseMoneyTest.Models;

namespace WiseMoneyTest.Repository
{
    public class AccountRepository
    {
        private List<Account> AccountList = new List<Account>
        {

        };

        private List<Transactions> TransactionsList = new List<Transactions>
        {

        };

        public void CreateAccount(Account account)
        {
            int lastId;
            
            if(AccountList.Count > 0)
            {
                lastId = AccountList.Max(x => x.AccountId);
            } else
            {
                lastId = 0;
            }

            account.AccountId = lastId;

            AccountList.Add(account);
        }

        public decimal GetBalance(Account account)
        {
            return account.Balance;
        }

        public Transactions Transfer(Account accountSending, Account accountReceiving, decimal value)
        {

            Transactions transactionDebt = new Transactions(accountSending, "Debt", value, DateTime.Now);
            Transactions transactionCredit = new Transactions(accountReceiving, "Credit", value, DateTime.Now);
            
            TransactionsList.Add(transactionDebt);
            TransactionsList.Add(transactionCredit);

            accountSending.Balance -= value;
            accountReceiving.Balance += value;
        }

        public Transactions GetBankStatement(Account account, DateTime startingDate, DateTime finishDate)
        {
            var accountTransactions = TransactionsList.Where(x => x.Account.AccountId == account.AccountId && x.Date >= startingDate && x.Date <= finishDate);
            

        }
    }
