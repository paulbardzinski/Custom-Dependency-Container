namespace App1.Services
{
    public partial class AuthenticationService
    {
        public static class AuthenticationMessages
        {
            public static string InvalidCredentials { get; private set; } = "Invalid email, username or password!";
            public static string NotLoggedIn { get; private set; } = "You are not logged in!";
            public static string UserAlreadyExists { get; private set; } = "User already exists!";
        }
    }
}
