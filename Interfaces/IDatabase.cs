using App1.Data;

namespace App1.Interfaces
{
    public interface IDatabase
    {
        public string DataFileDirectory { get; }

        public object? FindUser(string username, string password);
        public object? CreateUser(string email, string username, string password);
    }
}