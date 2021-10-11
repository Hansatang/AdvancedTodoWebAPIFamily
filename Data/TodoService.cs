using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Models;

namespace AdvancedTodoWebAPI.Data {
public class TodoService : ITodosService {

    private string todoFile = "families.json";
    private IList<Family> families;

    public TodoService() {
        if (!File.Exists(todoFile)) {
            WriteTodosToFile();
        } else {
            string content = File.ReadAllText(todoFile);
            families = JsonSerializer.Deserialize<List<Family>>(content);
        }
    }

    public async Task<IList<Family>> GetTodosAsync() {
        List<Family> tmp = new List<Family>(families);
        return tmp;
    }

    public async Task<Family> AddTodoAsync(Family family) {
        int max = families.Max(todo => todo.Id);
        family.Id = (++max);
        families.Add(family);
        WriteTodosToFile();
        return family;
    }

    public async Task RemoveTodoAsync(int todoId) {
        Family toRemove = families.First(t => t.Id == todoId);
        families.Remove(toRemove);
        WriteTodosToFile();
    }

    public async Task<Family> UpdateAsync(Family family) {
        Family toUpdate = families.FirstOrDefault(t => t.Id == family.Id);
        if(toUpdate == null) throw new Exception($"Did not find todo with id: {family.Id}");
        WriteTodosToFile();
        return toUpdate;
    }

    private void WriteTodosToFile() {
        string productsAsJson = JsonSerializer.Serialize(families);
        
        File.WriteAllText(todoFile, productsAsJson);
    }
}
}