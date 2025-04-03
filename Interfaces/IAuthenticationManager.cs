namespace App1.Interfaces
{
    public interface IAuthenticationManager
    {
        bool IsLoggedIn { get; }

        void Begin(bool startAtLoginPage = true);
    }
}