using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Application.Dto;
using User.Infrastructure.Persistence.Read.Context;

namespace User.Infrastructure.Persistence.Read.Configuration
{
    public sealed class RoleDtoConfiguration : IEntityTypeConfiguration<RoleDto>
    {
        private const string TableName = "Roles";
        
        public void Configure(EntityTypeBuilder<RoleDto> builder)
        {
            builder
                .ToTable(TableName, UserReadModelContext.SchemaName)
                .HasKey(role => role.Id);

            builder
                .Property(role => role.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
