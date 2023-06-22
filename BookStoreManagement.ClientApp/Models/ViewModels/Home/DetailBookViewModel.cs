using BookStoreManagement.ClientApp.Models.DTO;
using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models.ViewModels.Home
{
    public class DetailBookViewModel
    {
        public BookDto Book { get; set; }
        public AdditionViewModel Addition { get; set; }
        public CommentViewModel CommentViewModel { get; set; }
    }
}
