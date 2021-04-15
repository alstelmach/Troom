using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Infrastructure.Persistence.Read.Context;
using User.Infrastructure.Persistence.Read.Entities;

namespace User.Infrastructure.Persistence.Read.Configuration
{
    public sealed class RolePermissionAssignmentConfiguration : IEntityTypeConfiguration<RolePermissionAssignment>
    {
        private const string TableName = "PermissionRoleAssignments";
        
        public void Configure(EntityTypeBuilder<RolePermissionAssignment> builder)
        {
            builder
                .ToTable(TableName, UserReadModelContext.SchemaName)
                .HasKey(assignment => new { assignment.RoleId, assignment.PermissionId });

            builder
                .Property(assignment => assignment.PermissionId)
                .IsRequired();

            builder
                .Property(assignment => assignment.RoleId)
                .IsRequired();
        }
    }
}
