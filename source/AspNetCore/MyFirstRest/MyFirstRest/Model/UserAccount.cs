namespace MyFirstRest.Model
{
    public class UserAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public OrderSession Session { get; set; }

        private static UserAccount? _defaultUser;

        public UserAccount()
        {
            Session = new OrderSession();
        }

        public static UserAccount Default()
        {
            if (_defaultUser != null) return _defaultUser;
            return _defaultUser = new UserAccount
            {
                Email = "info@validdata.de",
                Password = "123"
            };

        }
    }
}