namespace BookStoreManagement.Core.Entities
{
    public class BookCategories
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category {  get; set; }
    }
}
