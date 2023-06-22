using System;
using System.Collections.Generic;

namespace BookStoreManagement.Core.DTO
{
    public class PostCommentDto
    {
        public int BookId { get; set; }
        public Guid? UserId { get; set; }
        public string AnonymousName { get; set; }
        public string Content { get; set; }
    }
    public class UserCommentDto
    {
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
    }
    public class CommentDto
    {
        public int Id {  get; set; }
        public int? BookId { get; set; }
        public Guid? UserId { get; set; }
        public UserCommentDto User { get; set; }
        public string AnonymousName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
