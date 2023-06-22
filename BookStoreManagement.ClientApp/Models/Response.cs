using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models
{
    public class Response<T>
    {
        public bool IsSucceeded { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; } = default;
    }
}
