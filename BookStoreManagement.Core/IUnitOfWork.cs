using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace BookStoreManagement.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IAuthorRepository Authors { get; }
        IPublisherRepository Publishers { get; }
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        IGenericRepository<Role> Roles { get; }
        ICommentRepository Comments { get; }
        IGenericRepository<RefreshToken> RefreshTokens { get; }

        Task<int> SaveAsync();
    }
}
