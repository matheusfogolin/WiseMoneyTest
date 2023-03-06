namespace WiseMoneyTest.Models.Account
{
    public class CreateAccountResponse
    {
        public CreateAccountResponse(int accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public int AccountNumber { get; set; }
    }
}
