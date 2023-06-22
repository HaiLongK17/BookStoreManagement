using System.Collections.Generic;

namespace BookStoreManagement.Core.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
