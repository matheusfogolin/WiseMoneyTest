using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Repository
{
    public class UserRepository
    {
        private static List<User> UsersList = new List<User>();
        public User CreateUser(User user)
        {
            int lastId;

            if (UsersList.Count > 0)
            {
                lastId = UsersList.Max(x => x.UserId);
            }
            else
            {
                lastId = 0;
            }

            user.UserId = lastId + 1;

            UsersList.Add(user);

            return user;
        }

        public User? GetUser(string emailAdress, string password)
        {
            return UsersList.SingleOrDefault(x => x.EmailAdress.ToLower() == emailAdress.ToLower() && x.Password == password);
        }

        public bool EmailAlreadyRegistered(string email)
        {
            return UsersList.FirstOrDefault(x => x.EmailAdress == email) == null ? false : true;
        }
    }
}
