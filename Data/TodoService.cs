using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Models;

namespace AdvancedTodoWebAPI.Data {
public class TodoService : ITodosService {

    private string todoFile = "adults.json";
    private IList<Adult> families;

    public TodoService() {
        if (!File.Exists(todoFile)) {
            WriteTodosToFile();
        } else {
            string content = File.ReadAllText(todoFile);
            families = JsonSerializer.Deserialize<List<Adult>>(content);
        }
    }

    public async Task<IList<Adult>> GetTodosAsync() {
        List<Adult> tmp = new List<Adult>(families);
        return tmp;
    }

    public async Task<Adult> AddTodoAsync(Adult family) {
        int max = families.Max(todo => todo.Id);
        family.Id = (++max);
        families.Add(family);
        WriteTodosToFile();
        return family;
    }

    public async Task RemoveTodoAsync(int todoId) {
        Adult toRemove = families.First(t => t.Id == todoId);
        families.Remove(toRemove);
        WriteTodosToFile();
    }

    public async Task<Adult> UpdateAsync(Adult family) {
        Adult toUpdate = families.FirstOrDefault(t => t.Id == family.Id);
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