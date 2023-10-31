using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WiseMoneyTest.Entities
{
    public class Account
    {
        public Account(int accountNumber, Guid userId)
        {
            AccountNumber = accountNumber;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
