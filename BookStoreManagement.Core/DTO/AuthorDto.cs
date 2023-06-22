namespace BookStoreManagement.Core.DTO
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateAuthorDto : CreateAuthorDto { }
    public class AuthorDto : CreateAuthorDto
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
    }
}
