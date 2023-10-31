using WiseMoneyTest.Models.Account;

namespace WiseMoneyTest.Services.Interfaces
{
    public interface IAccountService
    {
        public int GenerateRandomAccountNumber();
        public CreateAccountResponse CreateAccount(Guid userId);
        public GetBalanceResponse GetBalance(int accountNumber, Guid userId);
        public Guid FindUserFromRequest();

    }
}
