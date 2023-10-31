namespace WiseMoneyTest.Entities
{
    public class User
    {
        public User(string emailAdress, string password)
        {
            EmailAdress = emailAdress;
            Password = password;
        }

        public Guid Id { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
