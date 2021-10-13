using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace AdvancedTodoWebAPI.Data
{
    public interface IUserService
    {
        Task<IList<User>> GetUsersAsync();
        Task<User> ValidateUser(string userName, string passWord);
        Task<User> AddUserAsync(User user);
    }
}