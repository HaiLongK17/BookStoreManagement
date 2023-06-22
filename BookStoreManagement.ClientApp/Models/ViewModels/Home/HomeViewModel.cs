using BookStoreManagement.ClientApp.Models.DTO;
using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<BookDto> Books { get; set; } = new();
        public AdditionViewModel Addition { get; set; }
    }
}
