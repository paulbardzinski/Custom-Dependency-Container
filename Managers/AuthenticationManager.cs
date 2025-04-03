using App1.Interfaces;
using App1.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAuthenticationService _authService;
        public bool IsLoggedIn
        {
            get => _authService.IsLoggedIn;
        }

        public AuthenticationManager(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public void Begin(bool startAtLoginPage = true)
        {
            MessageProvider.AuthenticationGreeting(startAtLoginPage);

            string input = Console.ReadLine() ?? string.Empty;
            if (ShouldRedirectToOppositePage(startAtLoginPage, input))
            {
                Begin(!startAtLoginPage);
                return;
            }

            string[] credentials = input.Split(' ');
            if (CheckCredentials(startAtLoginPage, credentials))
            {
                MessageProvider.InvalidInputFormat();
                if (MessageProvider.TryAgain())
                {
                    Begin(startAtLoginPage);
                }
                return;
            }

            var response = startAtLoginPage
                ? _authService.Login(username: credentials[0], password: credentials[1])
                : _authService.Register(email: credentials[0], username: credentials[1], password: credentials[2]);

            if (!response.Status)
            {
                MessageProvider.AuthOperationFailed(response.Message, startAtLoginPage);
                if (MessageProvider.TryAgain())
                {
                    Begin(startAtLoginPage);
                }
                return;
            }

            MessageProvider.AuthOperationSuccessful(startAtLoginPage);

            if (IsLoggedIn)
                MessageProvider.ShowUserData(user: _authService.CurrentUser);
        }

        private static bool ShouldRedirectToOppositePage(bool startAtLoginPage, string input)
        {
            return (startAtLoginPage && input.Equals("register", StringComparison.OrdinalIgnoreCase))
                || (!startAtLoginPage && input.Equals("login", StringComparison.OrdinalIgnoreCase));
        }

        private static bool CheckCredentials(bool startAtLoginPage, string[] credentials)
        {
            return (startAtLoginPage && credentials.Length != 2)
                || (!startAtLoginPage && credentials.Length != 3);
        }
    }
}
