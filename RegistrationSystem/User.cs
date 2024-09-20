namespace RegistrationSystem
{
    class User
    {
        public string Login { get; }
        public string Password { get; }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public static bool CheckCorrectSymb(string login, string password)
        {
            if (login.Contains("~") || password.Contains("~"))
            {
                return false;
            }

            if (login == "" || password == "")
            {
                return false;
            }

            return true;
        }
    }
}
