using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        public List<Transactions> GetBankStatement(int accountNumber, DateTime startingDate, DateTime finishDate);
        public void AddTransaction(Transactions transaction);
    }
}
