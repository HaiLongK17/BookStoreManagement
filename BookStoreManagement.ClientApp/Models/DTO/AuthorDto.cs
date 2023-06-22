namespace BookStoreManagement.ClientApp.Models.DTO
{
    public class HandleAuthor
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateAuthorDto : HandleAuthor { }
    public class UpdateAuthorDto : HandleAuthor { }
    public class AuthorDto : HandleAuthor
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
    }
}
