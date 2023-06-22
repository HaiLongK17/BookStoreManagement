using BookStoreManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreManagement.Data.Configurations
{
    public class BookCategoriesConfiguration : IEntityTypeConfiguration<BookCategories>
    {
        public void Configure(EntityTypeBuilder<BookCategories> builder)
        {
            builder.ToTable("BookCategories");

            builder.HasKey(x => new { x.CategoryId, x.BookId });

            builder.HasOne(x => x.Category).WithMany(x => x.BookCategories)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Book).WithMany(x => x.BookCategories)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
