using App1.Data;
using App1.Structures;
using Response = App1.Structures.AuthenticationService.Response;

namespace App1.Interfaces
{
    public interface IAuthenticationService
    {
        public bool IsLoggedIn { get; }
        public UserModel? CurrentUser { get; }

        Response Login(string username, string password);
        Response Logout();
        Response Register(string email, string username, string password);
    }
}