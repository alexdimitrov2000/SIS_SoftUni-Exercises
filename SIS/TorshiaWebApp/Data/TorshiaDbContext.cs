namespace TorshiaWebApp.Data
{
    using Models;

    using Microsoft.EntityFrameworkCore;

    public class TorshiaDbContext : DbContext
    {
        public TorshiaDbContext()
        {
        }

        public TorshiaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=DESKTOP-TI6GEI6\SQLEXPRESS;Database=Torshia;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
