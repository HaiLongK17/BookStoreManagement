using BookStoreManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookStoreManagement.Data.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Publisher).WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Author).WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Summary).IsRequired();
            builder.Property(x => x.PhotoUrl).IsRequired().HasMaxLength(250);
            builder.Property(x => x.PhotoPublicId).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).HasColumnType("Money").HasDefaultValue(0.0);
            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.IsActice).IsRequired();
        }
    }
}
