using WiseMoneyTest.Entities;
using WiseMoneyTest.Entities.Login;
using WiseMoneyTest.Models.User;

namespace WiseMoneyTest.Services.Interfaces
{
    public interface IUserService
    {
        public string GenerateToken(User user);
        public LoginResponse? Authenticate(LoginInputModel loginInputModel);
        public void ValidateEmail(string email);
        public void CreateUser(CreateUserInputModel createUserInputModel);
    }
}
