using WiseMoneyTest.Data;
using WiseMoneyTest.Entities;
using WiseMoneyTest.Repository.Interfaces;

namespace WiseMoneyTest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WiseMoneyTestDbContext _context;

        public UserRepository(WiseMoneyTestDbContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User? GetUser(string emailAdress, string password)
        {
            return _context.Users.SingleOrDefault(x => x.EmailAdress.ToLower() == emailAdress.ToLower() && x.Password == password);
        }

        public bool EmailAlreadyRegistered(string email)
        {
            return _context.Users.FirstOrDefault(x => x.EmailAdress == email) == null ? false : true;
        }
    }
}
