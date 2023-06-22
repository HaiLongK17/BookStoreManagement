using AutoMapper;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Core.Wrappers;
using BookStoreManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreManagement.Core.Constants;

namespace BookStoreManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPhotoAccessor _photoAccessor;
        public UserService(IMapper mapper, UserManager<User> userManager,
            RoleManager<Role> roleManager, IPhotoAccessor photoAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _photoAccessor = photoAccessor;
        }

        public async Task<Response<int>> ChangePassword(Guid userId, ChangePasswordDto passwordDto)
        {
            List<string> errors = new();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            if (!string.IsNullOrEmpty(passwordDto.Password) && !string.IsNullOrEmpty(passwordDto.NewPassword))
            {
                var passwordChanged = await _userManager.ChangePasswordAsync(user, passwordDto.Password, passwordDto.NewPassword);

                if (!passwordChanged.Succeeded)
                {
                    errors.Add(Errors.INCORRECT_PASSWORD + " or new password does not meet required");
                    return Response<int>.Failure(Messages.BAD_REQUEST, errors);
                }

                return Response<int>.Success(200, 1);
            }

            errors.Add("Password or New Password field is required");
            return Response<int>.Failure(Messages.BAD_REQUEST, errors);
        }

        public async Task<Response<int>> CreateUser(CreateUserDto userDto)
        {
            List<string> errors = new();

            var roleCheck = await _roleManager.RoleExistsAsync(userDto.Role);

            if (!roleCheck)
            {
                errors.Add("The role you requested does not exist");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            var user = _mapper.Map<User>(userDto);

            var uploadResult = await _photoAccessor.AddPhoto(null, Applications.USER);

            user.Avatar = uploadResult.PhotoUrl;
            user.AvatarPublicId = uploadResult.PublicId;

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                userDto.Password = "BookStore@0110";
            }

            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();

            await _userManager.SetLockoutEnabledAsync(user, false);

            var createUser = await _userManager.CreateAsync(user, userDto.Password);

            if (!createUser.Succeeded)
            {
                errors.Add(Errors.ADD_FAILURE);
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            await _userManager.AddToRoleAsync(user, userDto.Role);

            return Response<int>.Success(200, 1);
        }

        public async Task<Response<List<UserDto>>> GetUsersInRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            var result = _mapper.Map<List<UserDto>>(users);

            foreach(var user in result)
            {
                user.Role = role;
            }

            return Response<List<UserDto>>.Success(200, result);
        }

        public async Task<Response<UserDto>> GetUserById(Guid userId)
        {
            List<string> errors = new();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<UserDto>.Failure(Messages.NOT_FOUND, errors);
            }

            var role = await _userManager.GetRolesAsync(user);

            var result = _mapper.Map<UserDto>(user);

            result.Role = role[0]; 

            return Response<UserDto>.Success(200, result);
        }

        public async Task<Response<UserDto>> UpdateUser(Guid userId, UpdateUserDto userDto)
        {
            List<string> errors = new();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<UserDto>.Failure(Messages.NOT_FOUND, errors);
            }

            _mapper.Map(userDto, user);

            user.UserName = user.Email;

            var currentRole = await _userManager.GetRolesAsync(user);

            if (!string.IsNullOrEmpty(userDto.Role) || currentRole[0] != userDto.Role)
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole[0]);
                await _userManager.AddToRoleAsync(user, userDto.Role);
            }

            if (userDto.File != null)
            {
                if(user.AvatarPublicId != Applications.DEFAUlT_USER_AVATAR_ID)
                {
                    var deleteResult = await _photoAccessor.DeletePhoto(user.AvatarPublicId);
                    if(deleteResult != "ok")
                    {
                        errors.Add(Errors.DELETE_PHOTO_FAILURE);
                        return Response<UserDto>.Failure(Messages.BAD_REQUEST, errors);
                    }
                }

                var uploadResult = await _photoAccessor.AddPhoto(userDto.File, Applications.USER);

                user.Avatar = uploadResult.PhotoUrl;
                user.AvatarPublicId = uploadResult.PublicId;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                errors.Add(Errors.UPDATE_FAILURE);
                return Response<UserDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var userResult = _mapper.Map<UserDto>(user);

            return Response<UserDto>.Success(200, userResult);
        }

        public async Task<Response<int>> UnlockUser(Guid userId)
        {
            List<string> errors = new();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            user.LockoutEnd = DateTime.Now;
            user.Status = true;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                errors.Add(Errors.UPDATE_FAILURE);
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            return Response<int>.Success(200, 1);
        }

        public async Task<Response<int>> LockUser(Guid userId)
        {
            List<string> errors = new();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            user.LockoutEnd = DateTime.Now.AddYears(1000);
            user.Status = false;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                errors.Add(Errors.UPDATE_FAILURE);
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            return Response<int>.Success(200, 1);
        }
    }
}
