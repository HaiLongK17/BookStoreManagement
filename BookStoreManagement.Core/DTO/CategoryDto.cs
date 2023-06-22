namespace BookStoreManagement.Core.DTO
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateCategoryDto : CreateCategoryDto { }
    public class CategoryDto : CreateCategoryDto
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
    }
}
