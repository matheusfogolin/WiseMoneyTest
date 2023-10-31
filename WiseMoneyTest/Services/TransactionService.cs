using WiseMoneyTest.Entities;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.Transactions;
using WiseMoneyTest.Repository;
using WiseMoneyTest.Repository.Interfaces;
using WiseMoneyTest.Services.Interfaces;

namespace WiseMoneyTest.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public void Transfer(TransferInputModel transferInputModel, Guid userId)
        {
            var accountSending = _accountRepository.GetAccount(transferInputModel.SendingAccountNumber, userId);
            var accountReceiving = _accountRepository.GetAccount(transferInputModel.ReceivingAccountNumber);

            if (CheckIfTryingToTransferToTheSameAccount(accountSending.AccountNumber, accountReceiving.AccountNumber))
                throw new AccountValidationException("Não é permitido fazer transferência entre a mesma conta.");

            if (accountSending == null)
                throw new AccountValidationException("Conta não existe ou não pertence ao usuário");

            if (!HasEnoughAccountBalance(accountSending.Balance, transferInputModel.TransferValue))
                throw new AccountValidationException("Saldo insuficiente para transferência.");

            Transactions transactionDebt = new Transactions
                (accountSending.AccountNumber, TransactionEnum.Debt, transferInputModel.TransferValue, DateTime.Now);
            Transactions transactionCredit = new Transactions
                (accountReceiving.AccountNumber, TransactionEnum.Credit, transferInputModel.TransferValue, DateTime.Now);

            _transactionRepository.AddTransaction(transactionDebt);
            _transactionRepository.AddTransaction(transactionCredit);

            _accountRepository.UpdateBalanceAfterTransaction(accountSending, accountReceiving, transferInputModel.TransferValue);
        }

        public void Deposit(DepositInputModel depositInputModel)
        {
            var accountToUpdate = _accountRepository.GetAccount(depositInputModel.AccountNumber);
            if (accountToUpdate == null)
                throw new AccountNotFoundException("Conta não existe ou não pertence ao seu usuário.");
            
            Transactions deposit = new Transactions(accountToUpdate.AccountNumber, TransactionEnum.Credit, depositInputModel.Value, DateTime.Now);

            _transactionRepository.AddTransaction(deposit);
            _accountRepository.UpdateBalanceAfterDeposit(accountToUpdate, depositInputModel.Value);
        }

        public List<Transactions> GetBankStatement(int accountNumber, DateTime startingDate, DateTime finishDate)
        {
            var accountTransactions = _transactionRepository.GetBankStatement(accountNumber, startingDate, finishDate);
            if (accountTransactions == null)
                throw new AccountValidationException("Não há transações para essa conta nesse intervalo");

            return accountTransactions;
        }

        public bool HasEnoughAccountBalance(decimal balance, decimal transferValue) => balance >= transferValue;

        public bool CheckIfTryingToTransferToTheSameAccount(int accountNumberSending, int accountNumberReceiving) => accountNumberSending == accountNumberReceiving;
    }
}
