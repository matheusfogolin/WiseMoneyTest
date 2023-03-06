namespace WiseMoneyTest.Models.Account
{
    public class GetBalanceResponse
    {
        public GetBalanceResponse(decimal balance)
        {
            Balance = balance;
        }

        public decimal Balance { get; set; }
    }
}
