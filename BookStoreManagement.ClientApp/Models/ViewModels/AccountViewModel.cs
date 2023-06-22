using BookStoreManagement.ClientApp.Models.DTO;

namespace BookStoreManagement.ClientApp.Models.ViewModels
{
    public class AccountViewModel
    {
        public UserDto User { get; set; }

        public ChangePasswordDto ChangePassword { get; set; }
    }
}
