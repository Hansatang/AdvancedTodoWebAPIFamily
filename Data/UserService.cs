using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedTodoWebAPI.Models.Persistence;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AdvancedTodoWebAPI.Data
{
    public class UserService : IUserService
    {

        private Task<IList<User>> users;

        public UserService()
        {
            users = GetUsersAsync();
        }
        
        public async Task<User> ValidateUser(string userName, string passWord)
        {
            User user = null;
            using (AdultContext lb = new AdultContext())
            {
                user = await lb.Users.FirstOrDefaultAsync(tempUser => tempUser.UserName.Equals(userName) && tempUser.Password.Equals(passWord));
            }
            return user;
        }
        
        public async Task<IList<User>> GetUsersAsync()
        {
            using (AdultContext lb = new AdultContext())
            {
                IList<User> users = lb.Users.Include(user => user.UserName).ToList();
                return users;
            }
        }

        public async Task<User> AddUserAsync(User user)
        {
            Console.WriteLine("Add");
            using (AdultContext lb = new AdultContext())
            {
                User tempUser = new User
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Domain = user.Domain,
                    Role = user.Role,
                    SecurityLevel = user.SecurityLevel
                };
                await lb.Users.AddAsync(tempUser);

                await lb.SaveChangesAsync();
                return tempUser;
            }
        }
        
        /*
        public async Task RemoveUserAsync(string username)
        {
            Console.WriteLine("Remove");
            using (AdultContext lb = new AdultContext())
            {
                lb.Users.Remove(new User() {UserName = username});
                await lb.SaveChangesAsync();
            }
        }
        
        public async Task<User> UpdateAsync(User user)
        {
            Console.WriteLine("Update");
            using (AdultContext lb = new AdultContext())
            {
                lb.Entry(await lb.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName)).CurrentValues.SetValues(user);
                await lb.SaveChangesAsync();
                return user;
            }
        }*/
        
        
    }
}