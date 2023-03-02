namespace WiseMoneyTest.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; private set; }
        public User User { get; private set; }
        public decimal Balance { get; set; }
    }
}
