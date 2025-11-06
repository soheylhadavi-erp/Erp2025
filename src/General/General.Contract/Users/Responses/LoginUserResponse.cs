namespace General.Contract.Auth.Users
{
    public class LoginUserResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
