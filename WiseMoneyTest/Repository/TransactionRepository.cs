using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Repository
{
    public class TransactionRepository
    {
        private static List<Transaction> TransactionsList = new List<Transaction>();

        public List<Transaction> GetBankStatement(int accountNumber, DateTime startingDate, DateTime finishDate)
        {
            var accountTransactions = TransactionsList.Where(x => x.Account.AccountNumber == accountNumber && x.Date >= startingDate && x.Date <= finishDate).ToList();

            return accountTransactions;
        }

        public void AddTransactionToList(Transaction transaction)
        {
            TransactionsList.Add(transaction);
        }
    }
}
