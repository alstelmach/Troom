using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Infrastructure.Persistence.Read.Configuration
{
    public sealed class UserEntityConfiguration : IEntityTypeConfiguration<Entities.User>
    {
        public void Configure(EntityTypeBuilder<Entities.User> builder)
        {
            builder
                .Property(user => user.Login)
                .HasColumnType("varchar(1000)")
                .IsRequired();
            
            builder
                .Property(user => user.Password)
                .HasColumnType("bytea")
                .IsRequired();
            
            builder
                .Property(user => user.FirstName)
                .HasColumnType("varchar(1000)")
                .IsRequired();
            
            builder
                .Property(user => user.LastName)
                .HasColumnType("varchar(1000)")
                .IsRequired();
            
            builder
                .Property(user => user.MailAddress)
                .HasColumnType("varchar(1000)")
                .IsRequired();
        }
    }
}
