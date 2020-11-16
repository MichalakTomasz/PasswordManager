using Microsoft.EntityFrameworkCore;
using PasswordManager.EntityModels;

namespace PasswordManager.Context
{
    public class PasswordDbContext : DbContext
    {
        //public PasswordDbContext(DbContextOptions<PasswordDbContext> options) : base(options)
        //{
        //    ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}

        public DbSet<PasswordSet> Passwords { get; set; }
        public DbSet<User> Users { get; set; }

        public PasswordDbContext()
            => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlite(@"Data Source =..\PasswordDbSqlite.db");
        }
    }
}
