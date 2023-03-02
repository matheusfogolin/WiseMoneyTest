﻿namespace WiseMoneyTest.Models
{
    public class Transactions
    {
        public Transactions(Account account, string transactionType, decimal transactionValue, DateTime date)
        {
            Account = account;
            TransactionType = transactionType;
            TransactionValue = transactionValue;
            Date = date;
        }
        public Account Account { get; set; }
        public string TransactionType { get; set; }
        public decimal TransactionValue { get; set; }
        public DateTime Date {get; set; }
    }
}
