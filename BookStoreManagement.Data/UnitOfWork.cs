using BookStoreManagement.Core;
using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using BookStoreManagement.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace BookStoreManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext _context;
        private ICategoryRepository _categories;
        private IAuthorRepository _authors;
        private IPublisherRepository _publishers;
        private IBookRepository _books;
        private IUserRepository _users;
        private IGenericRepository<Role> _roles;
        private IGenericRepository<RefreshToken> _refreshToken;
        private ICommentRepository _comments;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);

        public IAuthorRepository Authors => _authors ??= new AuthorRepository(_context);

        public IPublisherRepository Publishers => _publishers ??= new PublisherRepository(_context);

        public IBookRepository Books => _books ??= new BookRepository(_context);

        public IUserRepository Users => _users ??= new UserRepository(_context);

        public IGenericRepository<Role> Roles => _roles ??= new GenericRepository<Role>(_context);

        public ICommentRepository Comments => _comments ??= new CommentRepository(_context);

        public IGenericRepository<RefreshToken> RefreshTokens => _refreshToken ??= new GenericRepository<RefreshToken>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
