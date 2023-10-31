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
using WiseMoneyTest.Repository.Interfaces;
using WiseMoneyTest.Services.Interfaces;

namespace WiseMoneyTest.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
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
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public LoginResponse? Authenticate(LoginInputModel loginInputModel)
        {
            var user = _userRepository.GetUser(loginInputModel.Email, loginInputModel.Password);
            if (user == null)
                throw new AccountNotFoundException("E-mail ou senha inválidos.");

            var token = GenerateToken(user);
            return new LoginResponse(token);
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

            if (_userRepository.EmailAlreadyRegistered(createUserInputModel.EmailAdress))
                throw new InvalidEmailException("O e-mail já existe na base de dados.");

            var user = new User(createUserInputModel.EmailAdress, createUserInputModel.Password);

            _userRepository.CreateUser(user);
        }
    }
}
