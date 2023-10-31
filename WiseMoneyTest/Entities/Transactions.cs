using System.Transactions;

namespace WiseMoneyTest.Entities
{
    public class Transactions
    {
        public Transactions(int accountNumber, TransactionEnum transactionType, decimal transactionValue, DateTime date)
        {
            AccountNumber = accountNumber;
            TransactionType = transactionType;
            TransactionValue = transactionValue;
            Date = date;
        }
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public TransactionEnum TransactionType { get; set; }
        public decimal TransactionValue { get; set; }
        public DateTime Date { get; set; }
    }
}
