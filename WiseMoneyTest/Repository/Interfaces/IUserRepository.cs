using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User CreateUser(User user);
        public User? GetUser(string emailAdress, string password);
        public bool EmailAlreadyRegistered(string email);
    }
}
