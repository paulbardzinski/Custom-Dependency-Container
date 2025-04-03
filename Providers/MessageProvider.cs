using App1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Providers
{
    public static class MessageProvider
    {
        public static void AuthenticationGreeting(bool startAtLoginPage = true)
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Welcome to the authentication service!");

            if (startAtLoginPage)
            {
                Console.WriteLine("Please enter your username/email and password divided with space.");
                Console.WriteLine("If you don't have an account, type 'register'.");
            }
            else
            {
                Console.WriteLine("Please enter your email, username and password divided with space.");
                Console.WriteLine("If you already have an account, type 'login'.");
            }
            Console.WriteLine("--------------------------------------------------");
        }

        public static void InvalidInputFormat()
        {
            Console.WriteLine("Provided invalid input format!");
        }

        public static void AuthOperationSuccessful(bool startAtLoginPage = true)
        {
            if (startAtLoginPage)
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Registration successful!");
            }

            Console.WriteLine();
        }

        public static void AuthOperationFailed(string message, bool startAtLoginPage = true)
        {
            if (startAtLoginPage)
            {
                Console.WriteLine($"Login failed! {message}");
            }
            else
            {
                Console.WriteLine($"Registration failed! {message}");
            }
            Console.WriteLine();
        }

        public static bool TryAgain()
        {
            Console.WriteLine("Try again? (Y/n)");
            var input = Console.ReadKey().KeyChar;
            if (input == 'y' || input == '\r')
            {
                Console.Clear();
                return true;
            }

            return false;
        }

        public static void ShowUserData(UserModel? user)
        {
            if (user == null)
            {
                Console.WriteLine("User is null!");
                return;
            }

            Console.WriteLine("Your data:");
            Console.WriteLine("------------------------------------------");
            foreach (var property in user.GetType().GetProperties())
            {
                Console.WriteLine($"{property.Name}: {property.GetValue(user)}");
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
