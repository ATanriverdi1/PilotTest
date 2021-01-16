using Microsoft.EntityFrameworkCore;
using PilotTest.API.Model;

namespace PilotTest.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {   }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Marketing.db");
        }

        public DbSet<Student> Students { get; set; }
    }
}
