using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Application.Dto.User;
using User.Infrastructure.Persistence.Read.Context;

namespace User.Infrastructure.Persistence.Read.Configuration
{
    public sealed class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
    {
        private const string TableName = "Users";
        
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            builder
                .ToTable(TableName, UserReadModelContext.SchemaName)
                .HasKey(user => user.Id);
            
            builder
                .Property(user => user.Login)
                .HasColumnType("varchar(100)")
                .IsRequired();
            
            builder
                .Property(user => user.Password)
                .HasColumnType("bytea")
                .HasMaxLength(256)
                .IsRequired();
            
            builder
                .Property(user => user.FirstName)
                .HasColumnType("varchar(100)")
                .IsRequired(false);
            
            builder
                .Property(user => user.LastName)
                .HasColumnType("varchar(100)")
                .IsRequired(false);
            
            builder
                .Property(user => user.MailAddress)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
