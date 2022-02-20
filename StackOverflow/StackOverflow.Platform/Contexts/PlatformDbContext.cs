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

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Comments)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.CommentVotes)
                .WithOne(c => c.Comment)
                .HasForeignKey(v => v.CommentId);
            
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers", x => x.ExcludeFromMigrations())
                .HasOne<CommentVote>()
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey<CommentVote>(c => c.ApplicationUserId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
    }
}
