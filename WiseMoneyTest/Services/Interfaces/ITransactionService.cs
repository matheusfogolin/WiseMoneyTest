using WiseMoneyTest.Entities;
using WiseMoneyTest.Models.Transactions;

namespace WiseMoneyTest.Services.Interfaces
{
    public interface ITransactionService
    {
        public void Transfer(TransferInputModel transferInputModel, Guid userId);
        public void Deposit(DepositInputModel depositInputModel);
        public List<Transactions> GetBankStatement(int accountNumber, DateTime startingDate, DateTime finishDate);
        public bool HasEnoughAccountBalance(decimal balance, decimal transferValue);
        public bool CheckIfTryingToTransferToTheSameAccount(int accountNumberSending, int accountNumberReceiving);

    }
}
