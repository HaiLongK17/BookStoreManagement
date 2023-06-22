namespace BookStoreManagement.ClientApp.Models.DTO
{
    public class HandlePublisher
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreatePublisherDto : HandlePublisher { }
    public class UpdatePublisherDto : HandlePublisher { }
    public class PublisherDto : HandlePublisher
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
    }
}
