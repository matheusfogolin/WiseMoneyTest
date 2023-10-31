using WiseMoneyTest.Data;
using WiseMoneyTest.Entities;
using WiseMoneyTest.Repository.Interfaces;

namespace WiseMoneyTest.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly WiseMoneyTestDbContext _context;

        public TransactionRepository(WiseMoneyTestDbContext context)
        {
            _context = context;
        }

        public List<Transactions> GetBankStatement(int accountNumber, DateTime startingDate, DateTime finishDate)
        {
            var accountTransactions = _context.Transactions.Where
                (x => x.AccountNumber == accountNumber && x.Date >= startingDate && x.Date <= finishDate).ToList();

            return accountTransactions;
        }

        public void AddTransaction(Transactions transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
