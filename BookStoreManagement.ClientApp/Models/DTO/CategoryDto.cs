namespace BookStoreManagement.ClientApp.Models.DTO
{
    public class HandleCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateCategoryDto : HandleCategory { }
    public class UpdateCategoryDto : HandleCategory { }
    public class CategoryDto : HandleCategory
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
    }
}
