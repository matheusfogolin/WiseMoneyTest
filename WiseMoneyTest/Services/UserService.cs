using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using WiseMoneyTest.Entities;
using WiseMoneyTest.Entities.Login;
using WiseMoneyTest.Exceptions;
using WiseMoneyTest.Models.User;
using WiseMoneyTest.Repository;

namespace WiseMoneyTest.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.EmailAdress.ToString()),
                    new Claim("UserId", user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public void ValidateEmail(string email)
        {
            var validateEmail = new Regex("^\\S+@\\S+\\.\\S+$");
            if (!validateEmail.IsMatch(email))
                throw new InvalidEmailException("E-mail inválido.");
        }
        public void CreateUser(CreateUserInputModel createUserInputModel)
        {
            ValidateEmail(createUserInputModel.EmailAdress);

            if (userRepository.EmailAlreadyRegistered(createUserInputModel.EmailAdress))
                throw new InvalidEmailException("O e-mail já existe na base de dados.");

            var user = new User(createUserInputModel.EmailAdress, createUserInputModel.Password);

            userRepository.CreateUser(user);
        }
        public LoginResponse? Authenticate(LoginInputModel loginInputModel)
        {
            var user = userRepository.GetUser(loginInputModel.User, loginInputModel.Password);
            if (user == null)
                throw new AccountNotFoundException("E-mail ou senha inválidos.");

            var token = GenerateToken(user);
            return new LoginResponse(token);
        }
    }
}
