using BookStoreManagement.ClientApp.Models.DTO;
using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models.ViewModels
{
    public class AdditionViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new();
        public List<AuthorDto> Authors { get; set; } = new();
        public List<PublisherDto> Publishers { get; set; } = new();
    }
}
