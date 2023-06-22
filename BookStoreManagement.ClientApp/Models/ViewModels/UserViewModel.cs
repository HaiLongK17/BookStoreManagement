using BookStoreManagement.ClientApp.Models.DTO;
using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models.ViewModels
{
    public class UserViewModel
    {
        public UserDto User { get; set; }
        public CreateUserDto NewUser { get; set; }
        public UpdateUserDto UpdateUser { get; set; }
        public ChangePasswordDto ChangePassword { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>
        {
            new RoleDto
            {
                Name = "SiteOwner"
            },
            new RoleDto
            {
                Name = "NormalUser"
            }
        };
    }
}
