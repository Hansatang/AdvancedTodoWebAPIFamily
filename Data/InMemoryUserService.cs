using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace AdvancedTodoWebAPI.Data
{
    public class InMemoryUserService : IUserService
    {
        private string usersFile = "users.json";
        private ICollection<User> users;

        public InMemoryUserService()
        {
            if (!File.Exists(usersFile))
            {
                WriteUsersToFile();
            }
            else
            {
                string content = File.ReadAllText(usersFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            List<User> tmp = new List<User>(users);
            return tmp;
        }

        public async Task<User> ValidateUser(string userName, string passWord)
        {
            User user = users.FirstOrDefault(u => u.UserName.Equals(userName) && u.Password.Equals(passWord));
            if (user != null)
            {
                return user;
            }

            throw new Exception("User not found");
        }

        public async Task<User> AddUserAsync(User user)
        {
            Console.WriteLine(user.UserName);
            users.Add(user);
            WriteUsersToFile();
            return user;
        }

        private void WriteUsersToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(users);

            File.WriteAllText(usersFile, productsAsJson);
        }
    }
}