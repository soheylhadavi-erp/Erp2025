namespace General.Contract.Auth.Users
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
