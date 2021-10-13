using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace AdvancedTodoWebAPI.Data
{
    public class AdultService : IAdultService
    {
        private string adultsFile = "adults.json";
        private IList<Adult> adults;

        public AdultService()
        {
            if (!File.Exists(adultsFile))
            {
                WriteAdultsToFile();
            }
            else
            {
                string content = File.ReadAllText(adultsFile);
                adults = JsonSerializer.Deserialize<List<Adult>>(content);
            }
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
            List<Adult> tmp = new List<Adult>(adults);
            return tmp;
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            int max = adults.Max(todo => todo.Id);
            adult.Id = (++max);
            adults.Add(adult);
            WriteAdultsToFile();
            return adult;
        }

        public async Task RemoveAdultAsync(int todoId)
        {
            Adult toRemove = adults.First(t => t.Id == todoId);
            adults.Remove(toRemove);
            WriteAdultsToFile();
        }

        public async Task<Adult> UpdateAsync(Adult adult)
        {
            Adult toUpdate = adults.FirstOrDefault(t => t.Id == adult.Id);
            adults[adult.Id] = adult;
            if (toUpdate == null) throw new Exception($"Did not find todo with id: {adult.Id}");
            WriteAdultsToFile();
            return toUpdate;
        }

        private void WriteAdultsToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(adults);

            File.WriteAllText(adultsFile, productsAsJson);
        }
    }
}