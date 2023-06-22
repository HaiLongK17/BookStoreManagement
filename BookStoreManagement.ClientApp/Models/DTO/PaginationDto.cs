namespace BookStoreManagement.ClientApp.Models.DTO
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }

    public class PaginatedResult<T>
    {
        public PaginatedResult(T data, Pagination pagination)
        {
            Data = data;
            Pagination = pagination;
        }

        public T Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}
