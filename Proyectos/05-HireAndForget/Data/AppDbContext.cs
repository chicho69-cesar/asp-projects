using Microsoft.EntityFrameworkCore;

namespace _05_HireAndForget.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}