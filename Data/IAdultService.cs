using System.Collections.Generic;
using System.Threading.Tasks;
using Models;


namespace AdvancedTodoWebAPI.Data
{
    public interface IAdultService
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<Adult> AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int todoId);
        Task<Adult> UpdateAsync(Adult adult);
    }
}