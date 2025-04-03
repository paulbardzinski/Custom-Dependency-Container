using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App1.Interfaces;
using App1.Data;
using Response = App1.Structures.AuthenticationService.Response;
using App1.Managers;

namespace App1.Services
{
    public partial class AuthenticationService : IAuthenticationService
    {
        private readonly string EncryptionKey = "AvJeNejlepsi";
        public bool IsLoggedIn { get; private set; } = false;
        public UserModel? CurrentUser { get; private set; }
        private readonly IDatabase _database;

        public AuthenticationService(IDatabase database)
        {
            _database = database;
        }

        public Response Login(string username, string password)
        {
            if (!IsUsernameValid(username) || !IsPasswordValid(password))
            {
                return new()
                {
                    Message = AuthenticationMessages.InvalidCredentials
                };
            }

            //var encryptedPassword = SymmetricEncryptionDecryptionManager.Encrypt(data: password, key: EncryptionKey);

            if (_database.FindUser(username: username, password: password) is not UserModel user)
            {
                return new()
                {
                    Message = AuthenticationMessages.InvalidCredentials
                };
            }

            CurrentUser = user;
            IsLoggedIn = true;
            return new()
            {
                Status = true,
            };
        }

        public Response Register(string email, string username, string password)
        {
            if (!IsEmailValid(email) || !IsUsernameValid(username) || !IsPasswordValid(password))
            {
                return new()
                {
                    Message = AuthenticationMessages.InvalidCredentials
                };
            }

            //var encryptedPassword = SymmetricEncryptionDecryptionManager.Encrypt(data: password, key: EncryptionKey);

            UserModel? user = _database.CreateUser(
                email: email,
                username: username,
                password: password) as UserModel;

            if (user == null)
            {
                return new()
                {
                    Message = AuthenticationMessages.UserAlreadyExists
                };
            }

            CurrentUser = user;
            IsLoggedIn = true;
            return new()
            {
                Status = true,
            };
        }

        private static bool IsEmailValid(string? email)
        {
            if (email == null)
            {
                return false;
            }

            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsUsernameValid(string? username)
        {
            if (username == null)
            {
                return false;
            }

            var trimmedUsername = username.Trim();
            return
                !trimmedUsername.Contains(' ')
                && trimmedUsername.Length >= 4
                && trimmedUsername.Length <= 64;
        }

        private static bool IsPasswordValid(string? password)
        {
            if (password == null)
            {
                return false;
            }

            var trimmedPassword = password.Trim();
            return
                !trimmedPassword.Contains(' ')
                && trimmedPassword.Length >= 8
                && trimmedPassword.Length <= 255;
        }

        public Response Logout()
        {
            if (IsLoggedIn)
            {
                IsLoggedIn = false;
                return new()
                {
                    Status = true,
                };

            }

            return new()
            {
                Message = AuthenticationMessages.NotLoggedIn
            };
        }
    }
}
