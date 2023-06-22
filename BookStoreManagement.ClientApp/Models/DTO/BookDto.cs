using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models.DTO
{
    public class BookCommonProperties
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActice { get; set; }
    }
    public class HandleBook : BookCommonProperties
    {
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public IFormFile File { get; set; }
        public int[] CategoryIds { get; set; }
    }
    public class CreateBookDto : HandleBook { }
    public class UpdateBookDto : HandleBook { }
    public class BookDto : BookCommonProperties
    {
        public int Id {  get; set; }
        public AuthorDto AuthorDto { get; set; }
        public PublisherDto PublisherDto { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
