using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Persistence.Read.Configuration;

namespace User.Infrastructure.Persistence.Read.Context
{
    public sealed class UserReadModelContext : DbContext
    {
        public const string SchemaName = "UserRead";
        
        public UserReadModelContext(DbContextOptions<UserReadModelContext> options)
            : base(options)
        {
        }
        
        public DbSet<Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfiguration(new UserEntityConfiguration());
    }
}
