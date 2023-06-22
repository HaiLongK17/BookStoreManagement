namespace BookStoreManagement.Core.DTO
{
    public class CreatePublisherDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdatePublisherDto : CreatePublisherDto { }
    public class PublisherDto : CreatePublisherDto
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
    }
}
