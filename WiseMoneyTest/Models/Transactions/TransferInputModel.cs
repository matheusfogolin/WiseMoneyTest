namespace WiseMoneyTest.Models.Transactions
{
    public class TransferInputModel
    {
        public TransferInputModel(int sendingAccountNumber, int receivingAccountNumber, decimal transferValue)
        {
            SendingAccountNumber = sendingAccountNumber;
            ReceivingAccountNumber = receivingAccountNumber;
            TransferValue = transferValue;
        }

        public int SendingAccountNumber { get; set; }
        public int ReceivingAccountNumber { get; set; }
        public decimal TransferValue { get; set; }
    }
}
