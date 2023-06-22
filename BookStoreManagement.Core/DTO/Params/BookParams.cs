namespace BookStoreManagement.Core.DTO.Params
{
    public class BookParams : PagingParams
    {
        public string Category { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
