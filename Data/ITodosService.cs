using System.Collections.Generic;
using System.Threading.Tasks;
using Models;


namespace AdvancedTodoWebAPI.Data {
public interface ITodosService {
    Task<IList<Family>> GetTodosAsync();
    Task<Family>   AddTodoAsync(Family family);
    Task   RemoveTodoAsync(int todoId);
    Task<Family>   UpdateAsync(Family family);
}
}