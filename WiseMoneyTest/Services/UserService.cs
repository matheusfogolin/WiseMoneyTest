using WiseMoneyTest.Models;
using WiseMoneyTest.Repository;

namespace WiseMoneyTest.Services
{
    public class UserService
    {
        public bool ValidateEmail(string email)
        {
            var emailAdressClass = new System.Net.Mail.MailAddress(email);

            return emailAdressClass.ToString() == email ? true : false;
        }
        public string CreateUser(User user)
        {
            var userRepository = new UserRepository();

            var validEmail = ValidateEmail(user.EmailAdress);

            if (validEmail)
            {
                userRepository.CreateUser(user);
                return "Usuário criado com sucesso.";
            }
            else
            {
                return "E-mail inválido.";
            }
        }

        public void MakeLogin(string login, string password)
        {
            var userRepository = new UserRepository();

            userRepository.MakeLogin(login, password);
        }
    }
}
