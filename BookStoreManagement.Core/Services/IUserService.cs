using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface IUserService
    {
        Task<Response<List<UserDto>>> GetUsersInRole(string role);
        Task<Response<UserDto>> GetUserById(Guid userId);
        Task<Response<int>> CreateUser(CreateUserDto userDto);
        Task<Response<UserDto>> UpdateUser(Guid userId, UpdateUserDto userDto);
        Task<Response<int>> ChangePassword(Guid userId, ChangePasswordDto passwordDto);
        Task<Response<int>> UnlockUser(Guid userId);
        Task<Response<int>> LockUser(Guid userId);
    }
}
