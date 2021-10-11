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
        private string todoFile = "users.json";
        private ICollection<User> users;

        public InMemoryUserService()
        {
            if (!File.Exists(todoFile))
            {
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }
        
        public async Task<IList<User>> GetTodosAsync() {
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

        public async Task<User> AddUserAsync(User adult)
        {
            Console.WriteLine(adult.UserName);
            users.Add(adult);
            WriteTodosToFile();
            return adult;
        }
        private void WriteTodosToFile() {
            string productsAsJson = JsonSerializer.Serialize(users);
        
            File.WriteAllText(todoFile, productsAsJson);
        }
    }
}


