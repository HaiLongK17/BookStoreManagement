using System.Collections.Generic;

namespace BookStoreManagement.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BookCategories> BookCategories { get; set; }
    }
}
