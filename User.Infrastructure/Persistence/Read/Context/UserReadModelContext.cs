using Microsoft.EntityFrameworkCore;
using User.Application.Dto;
using User.Infrastructure.Persistence.Read.Configuration;
using User.Infrastructure.Persistence.Read.Entities;

namespace User.Infrastructure.Persistence.Read.Context
{
    public sealed class UserReadModelContext : DbContext
    {
        public const string SchemaName = "userreadmodel";
        
        public UserReadModelContext(DbContextOptions<UserReadModelContext> options)
            : base(options)
        {
        }
        
        public DbSet<UserDto> Users { get; set; }
        public DbSet<RoleDto> Roles { get; set; }
        public DbSet<RolePermissionAssignment> RolePermissionAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfiguration(new RoleDtoConfiguration())
                .ApplyConfiguration(new UserDtoConfiguration())
                .ApplyConfiguration(new RolePermissionAssignmentConfiguration());
    }
}
