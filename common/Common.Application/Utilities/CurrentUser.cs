namespace Common.Application
{
    public static class CurrentUser
    {
        private static readonly AsyncLocal<string> _userId = new AsyncLocal<string>();
        private static readonly AsyncLocal<string> _userName = new AsyncLocal<string>();

        public static string UserId => _userId.Value;
        public static string UserName => _userName.Value;

        public static void Set(string userId, string userName)
        {
            _userId.Value = userId;
            _userName.Value = userName;
        }
    }
}
