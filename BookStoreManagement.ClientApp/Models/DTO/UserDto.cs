using Microsoft.AspNetCore.Http;
using System;

namespace BookStoreManagement.ClientApp.Models.DTO
{
    public class CommonUserProperty
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
    public class CreateUserDto : CommonUserProperty
    {
        public string Password { get; set; }
    }
    public class UpdateUserDto : CommonUserProperty
    {
        public IFormFile File { get; set; }
    }
    public class ChangePasswordDto
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
    public class UserDto : CommonUserProperty
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
        public string AvatarPublicId { get; set; }
    }
    public class RoleDto
    {
        public string Name { get; set; }
    }
}
