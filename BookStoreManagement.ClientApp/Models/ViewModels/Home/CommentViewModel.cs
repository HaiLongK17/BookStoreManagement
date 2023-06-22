using BookStoreManagement.ClientApp.Models.DTO;
using System;
using System.Collections.Generic;

namespace BookStoreManagement.ClientApp.Models.ViewModels.Home
{
    public class CommentViewModel
    {
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public PostCommentDto PostComment { get; set; }
    }
}
