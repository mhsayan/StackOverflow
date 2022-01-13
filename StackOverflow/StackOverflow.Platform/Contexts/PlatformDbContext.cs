using Microsoft.EntityFrameworkCore;
using StackOverflow.Membership.Entities;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Contexts
{
    public class PlatformDbContext : DbContext, IPlatformDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public PlatformDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers", x => x.ExcludeFromMigrations())
                .HasMany<Question>()
                .WithOne(x => x.ApplicationUser);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
    }
}
