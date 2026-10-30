using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence.Configurations;

public class TaskItemConfiguration : EntityConfiguration<TaskItem>
{
    public override void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Title).HasMaxLength(200).IsRequired();
        builder.Property(col => col.Description).IsRequired();
        builder.Property(col => col.CreatedAt).IsRequired();
        builder.Property(col => col.IsDeleted).IsRequired();

        builder.HasOne(col => col.Creator)
            .WithMany(col => col.CreatedTasks)
            .HasForeignKey(col => col.CreatorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(col => col.AssignedTo)
            .WithMany(col => col.AssignedTasks)
            .HasForeignKey(col => col.AssignedToId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        base.Configure(builder);
    }
}
