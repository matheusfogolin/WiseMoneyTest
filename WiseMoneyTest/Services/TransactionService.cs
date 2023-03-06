using WiseMoneyTest.Entities;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Transactions;
using WiseMoneyTest.Repository;

namespace WiseMoneyTest.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository transactionRepository;
        private readonly AccountRepository accountRepository;
        public TransactionService()
        {
            transactionRepository = new TransactionRepository();
            accountRepository = new AccountRepository();
        }
        public void Transfer(TransferInputModel transferInputModel, int userId)
        {
            var accountSending = accountRepository.GetAccount(transferInputModel.SendingAccountNumber, userId);
            var accountReceiving = accountRepository.GetAccount(transferInputModel.ReceivingAccountNumber);

            if (CheckIfTryingToTransferToTheSameAccount(accountSending.AccountNumber, accountReceiving.AccountNumber))
                throw new AccountValidationException("Não é permitido fazer transferência entre a mesma conta.");

            if (accountSending == null)
                throw new AccountValidationException("Conta não existe ou não pertence ao usuário");

            if (!HasEnoughAccountBalance(accountSending.Balance, transferInputModel.TransferValue))
                throw new AccountValidationException("Saldo insuficiente para transferência.");

            Transaction transactionDebt = new Transaction(accountSending, "Debt", transferInputModel.TransferValue, DateTime.Now);
            Transaction transactionCredit = new Transaction(accountReceiving, "Credit", transferInputModel.TransferValue, DateTime.Now);

            transactionRepository.AddTransactionToList(transactionDebt);
            transactionRepository.AddTransactionToList(transactionCredit);

            accountSending.Balance -= transferInputModel.TransferValue;
            accountReceiving.Balance += transferInputModel.TransferValue;
        }

        public void Deposit(DepositInputModel depositInputModel)
        {
            var accountToUpdate = accountRepository.GetAccount(depositInputModel.AccountNumber);
            if (accountToUpdate == null)
                throw new AccountNotFoundException("Conta não existe ou não pertence ao seu usuário.");
            
            Transaction deposit = new Transaction(accountToUpdate, "credit", depositInputModel.Value, DateTime.Now);

            transactionRepository.AddTransactionToList(deposit);
            accountToUpdate.Balance += depositInputModel.Value;
        }

        public List<Transaction> GetBankStatement(int accountNumber, DateTime startingDate, DateTime finishDate)
        {
            var accountTransactions = transactionRepository.GetBankStatement(accountNumber, startingDate, finishDate);
            if (accountTransactions == null)
                throw new AccountValidationException("Não há transações para essa conta nesse intervalo");

            return accountTransactions;
        }

        private bool HasEnoughAccountBalance(decimal balance, decimal transferValue) => balance >= transferValue;

        private bool CheckIfTryingToTransferToTheSameAccount(int accountNumberSending, int accountNumberReceiving) => accountNumberSending == accountNumberReceiving;
    }
}
