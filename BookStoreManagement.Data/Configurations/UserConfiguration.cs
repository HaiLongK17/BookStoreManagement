using BookStoreManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreManagement.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Avatar).IsRequired().HasMaxLength(250);
            builder.Property(x => x.AvatarPublicId).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PhoneNumber).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.NormalizedEmail).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.NormalizedUserName).HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(true);
        }
    }
}
