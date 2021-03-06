using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace AdvancedTodoWebAPI.Data
{
    public class InMemoryUserService : IUserService
    {
        private ICollection<User> users;

        public InMemoryUserService()
        {
            users = new List<User>();
            users.Add(new User
            {
                UserName  = "Troels",
                Password = "Troels1234"
            });
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
    }
}