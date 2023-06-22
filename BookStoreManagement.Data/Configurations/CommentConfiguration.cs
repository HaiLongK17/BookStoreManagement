using BookStoreManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookStoreManagement.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.User).WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Book).WithMany(x => x.Comments)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Content).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AnonymousName).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
