using System.Collections.Generic;
using System.Threading.Tasks;
using Models;


namespace AdvancedTodoWebAPI.Data {
public interface ITodosService {
    Task<IList<Adult>> GetTodosAsync();
    Task<Adult>   AddTodoAsync(Adult family);
    Task   RemoveTodoAsync(int todoId);
    Task<Adult>   UpdateAsync(Adult family);
}
}