using Microsoft.EntityFrameworkCore;
using PasswordManager.Context;

namespace PasswordManager.Services
{
    public class DbContextService : IDbContextService
    {
        private readonly DbContextOptions<PasswordDbContext> _options;

        public DbContextService(DbContextOptions<PasswordDbContext> options)
            => _options = options;
        public PasswordDbContext GetContext()
            => new PasswordDbContext(_options);
    }
}
