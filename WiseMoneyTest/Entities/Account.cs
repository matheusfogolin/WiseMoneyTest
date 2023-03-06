using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WiseMoneyTest.Entities
{
    public class Account
    {
        public Account(int userId, int accountNumber)
        {
            UserId = userId;
            AccountNumber = accountNumber;
        }

        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
