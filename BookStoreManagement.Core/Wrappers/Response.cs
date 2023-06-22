using System.Collections.Generic;

namespace BookStoreManagement.Core.Wrappers
{
    public class Response<T>
    {
        internal bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; } = default;

        public bool IsSucceeded() => Succeeded;

        public static Response<T> Success(int statusCode, T data)
            => new() { Succeeded = true, StatusCode = statusCode, Data = data };

        public static Response<T> Failure(string message, List<string> errors)
            => new() { Succeeded = false, Message = message, Errors = errors };
    }
}
