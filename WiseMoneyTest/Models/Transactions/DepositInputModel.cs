namespace WiseMoneyTest.Models.Transactions
{
    public class DepositInputModel
    {
        public DepositInputModel(int accountNumber, decimal value)
        {
            AccountNumber = accountNumber;
            Value = value;
        }

        public int AccountNumber { get; set; }
        public decimal Value { get; set; }
    }
}
