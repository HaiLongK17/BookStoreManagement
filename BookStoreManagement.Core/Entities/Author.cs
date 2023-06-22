using System.Collections.Generic;

namespace BookStoreManagement.Core.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Description {  get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
