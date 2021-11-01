using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AdvancedTodoWebAPI.Models.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Models;

namespace AdvancedTodoWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           //AddBookGenreAuthor();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static void AddBookGenreAuthor()
        {
            string content = File.ReadAllText("adults.json");
            IList<Adult> adults = JsonSerializer.Deserialize<List<Adult>>(content);
            using (AdultContext lb = new AdultContext())
            {
                foreach (Adult adult in adults)
                {
                    Job a = new Job
                    {
                        Id = adult.Id+1,
                        JobTitle = adult.JobTitle.JobTitle,
                        Salary = adult.JobTitle.Salary
                    };
                    Adult g = new Adult()
                    {
                        Id = adult.Id+1,
                        FirstName = adult.FirstName,
                        LastName = adult.LastName,
                        HairColor = adult.HairColor,
                        EyeColor = adult.EyeColor,
                        Age =adult.Age,
                        Weight = adult.Weight,
                        Height = adult.Height,
                        Sex = adult.Sex,
                        JobTitle = a
                    };
                    lb.Adults.Add(g);
                }
                lb.SaveChanges();
            }
        }
    }
}