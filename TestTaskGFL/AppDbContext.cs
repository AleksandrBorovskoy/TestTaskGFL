using Microsoft.EntityFrameworkCore;
using TestTaskGFL.Entities;

namespace TestTaskGFL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PersonEntity> Persons { get; set; }
    }
}
