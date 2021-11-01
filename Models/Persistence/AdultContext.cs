using Microsoft.EntityFrameworkCore;
using Models;

namespace AdvancedTodoWebAPI.Models.Persistence
{
    public class AdultContext : DbContext
    {
        public DbSet<Adult> Adults { get; set; }
        public DbSet<Job> Jobs { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // name of database
            optionsBuilder.UseSqlite(@"Data Source = Adult.db");
        }
    }
}