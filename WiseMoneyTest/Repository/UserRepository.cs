using WiseMoneyTest.Models;

namespace WiseMoneyTest.Repository
{
    public class UserRepository
    {
        private List<User> UsersList = new List<User>();

        public void CreateUser(User user)
        {
            UsersList.Add(user);
        }

        public string MakeLogin(string login, string password)
        {
            return UsersList.Where(x => x.EmailAdress == login && x.Password == password).Any() ? "Login realizado com sucesso." : "E-mail ou senha incorretos.";
        }
    }
}
