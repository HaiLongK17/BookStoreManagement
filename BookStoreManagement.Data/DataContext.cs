using BookStoreManagement.Core.Entities;
using BookStoreManagement.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Data
{
        public class DataContext : IdentityDbContext<User, Role, Guid>
        {
            public DataContext(DbContextOptions options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new AuthorConfiguration());
                modelBuilder.ApplyConfiguration(new CategoryConfiguration());
                modelBuilder.ApplyConfiguration(new PublisherConfiguration());
                modelBuilder.ApplyConfiguration(new BookConfiguration());
                modelBuilder.ApplyConfiguration(new UserConfiguration());
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
                modelBuilder.ApplyConfiguration(new CommentConfiguration());
                modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
                modelBuilder.ApplyConfiguration(new BookCategoriesConfiguration());

                modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
                modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
                modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);
                modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
                modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

                modelBuilder.Seed();
        }

            public override DbSet<Role> Roles { get; set; }
            public override DbSet<User> Users { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Publisher> Publishers { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<RefreshToken> RefreshTokens { get; set; }
            public DbSet<BookCategories> BookCategories { get; set; }
        }
    }
