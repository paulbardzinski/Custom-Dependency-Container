using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using App1.Data;
using App1.Interfaces;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using App1.Providers;

namespace App1.Services
{
    public class Database : IDatabase
    {
        public string DataFileDirectory
        {
            get => GetDataFileDirectory();
        }

        private string GetDataFileDirectory()
        {
            string fallbackValue = "../../../Data/users.json";

            var assembly = Assembly.GetExecutingAssembly();
            string? codeBase = assembly.CodeBase;
            if (codeBase == null)
            {
                return fallbackValue;
            }

            UriBuilder codeBaseUri = new UriBuilder(codeBase);
            string codeBasePath = Uri.UnescapeDataString(codeBaseUri.Path);

            var assemblyName = assembly.GetName().Name;
            if (assemblyName == null || !codeBasePath.Contains(assemblyName))
            {
                return fallbackValue;
            }

            string[] directories = codeBasePath.Split('/');
            int assemblyIndex = Array.LastIndexOf(directories, assemblyName);
            directories[assemblyIndex + 1] = "Data";
            directories[assemblyIndex + 2] = "users.json";
            return string.Join('/', directories, 0, assemblyIndex + 3);
        }

        public object? FindUser(string username, string password)
        {
            var userList = JsonSerializer.Deserialize<List<UserModel>>(File.ReadAllText(DataFileDirectory));
            var user = userList?.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public object? GetUserByID(ulong ID)
        {
            var userList = JsonSerializer.Deserialize<List<UserModel>>(File.ReadAllText(DataFileDirectory));
            var user = userList?.FirstOrDefault(u => u.ID == ID);
            return user;
        }

        public object? CreateUser(string email, string username, string password)
        {
            var userList = JsonSerializer.Deserialize<List<UserModel>>(File.ReadAllText(DataFileDirectory));
            var user = userList?.FirstOrDefault(user => user.Email == email);
            if (user != null)
            {
                return null;
            }

            user = new UserModel()
            {
                Email = email,
                Username = username,
                Password = password,
                ID = IDProvider.GetNextID()
            };

            userList?.Add(user);
            File.WriteAllText(DataFileDirectory, JsonSerializer.Serialize(userList));

            return user;
        }
    }
}
