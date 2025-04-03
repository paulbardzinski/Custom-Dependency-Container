
using App1.Interfaces;
using App1.Managers;

namespace App1;

internal static class Program
{
    static void Main()
    {
        new Startup()
            .ConfigureServices()
            .Configure();

        IAuthenticationService authService = ServiceManager.GetService<IAuthenticationService>();
        AuthenticationManager authManager = new AuthenticationManager(authService);
        authManager.Begin();

        Console.ReadKey();
    }
}

