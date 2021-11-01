using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodoWebAPI.Models.Persistence;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AdvancedTodoWebAPI.Data
{
    public class AdultService : IAdultService
    {
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            Console.WriteLine("Get");
            using (AdultContext lb = new AdultContext())
            {
                IList<Adult> first = lb.Adults.Include(adult => adult.JobTitle).ToList();
                return first;
            }
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            Console.WriteLine("Add");
            using (AdultContext lb = new AdultContext())
            {
                Job a = new Job
                {
                    Id = adult.Id + 1,
                    JobTitle = adult.JobTitle.JobTitle,
                    Salary = adult.JobTitle.Salary
                };
                Adult g = new Adult
                {
                    Id = adult.Id + 1,
                    FirstName = adult.FirstName,
                    LastName = adult.LastName,
                    HairColor = adult.HairColor,
                    EyeColor = adult.EyeColor,
                    Age = adult.Age,
                    Weight = adult.Weight,
                    Height = adult.Height,
                    Sex = adult.Sex,
                    JobTitle = a
                };
                lb.Adults.Add(g);

                await lb.SaveChangesAsync();
                return g;
            }
        }


        public async Task RemoveAdultAsync(int Id)
        {
            Console.WriteLine("Remove");
            using (AdultContext lb = new AdultContext())
            {
                lb.Adults.Remove(new Adult() {Id = Id});
                await lb.SaveChangesAsync();
            }
        }

        public async Task<Adult> UpdateAsync(Adult adult)
        {
            Console.WriteLine("Update");
            using (AdultContext lb = new AdultContext())
            {
                lb.Entry(await lb.Adults.FirstOrDefaultAsync(x => x.Id == adult.Id)).CurrentValues.SetValues(adult);
                await lb.SaveChangesAsync();
                return adult;

                // Adult adultToChange = lb.Adults.FirstOrDefault(adult => adult.Id == adult.Id);
                // adultToChange = adult;
                // await lb.SaveChangesAsync();
                // return adultToChange;
            }
        }
    }
}