using Microsoft.AspNetCore.Http;
using System;

namespace BookStoreManagement.Core.DTO
{
    public class CreateUserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UpdateUserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public IFormFile File { get; set; }
    }
    public class ChangePasswordDto
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
    public class UserDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public string Avatar { get; set; }
        public string AvatarPublicId { get; set; }
        public string Role { get; set; }
    }
}
