using BookStoreManagement.ClientApp.Models.DTO;
using System;

namespace BookStoreManagement.ClientApp.Models.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int CurrentPage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public HandleBook CurrentBook { get; set; }
        public HandleBook HandleBook { get; set; }
        public AdditionViewModel Addition { get; set; }
    }
}
