using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence.Configurations;

public class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(col => col.Id);

        builder.Property(col => col.FirstName).HasMaxLength(75).IsRequired();
        builder.Property(col => col.LastName).HasMaxLength(75).IsRequired();
        builder.Property(col => col.Email).HasMaxLength(256).IsRequired();
        builder.Property(col => col.PasswordHash).IsRequired();

        builder.Ignore(col => col.DisplayName);
        builder.HasIndex(col => col.Email).IsUnique();

        base.Configure(builder);
    }
}
