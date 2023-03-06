namespace WiseMoneyTest.Entities.Login
{
    public class LoginResponse
    {

        public LoginResponse(string token)
        {
            Token = token;
        }
        public string Token { get; private set; }
    }
}
