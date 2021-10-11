using System.Threading.Tasks;

using Models;

namespace AdvancedTodoWebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string userName, string passWord);
    }
}