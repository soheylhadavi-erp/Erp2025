namespace General.Contract.Users
{
    public class LoginUserResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
