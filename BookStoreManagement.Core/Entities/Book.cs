using System;
using System.Collections.Generic;

namespace BookStoreManagement.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title {  get; set; }

        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public int? PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public string Summary { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActice { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<BookCategories> BookCategories { get; set; } = new List<BookCategories>();
    }
}
