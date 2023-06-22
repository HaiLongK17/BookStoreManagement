using AutoMapper;
using BookStoreManagement.Core;
using BookStoreManagement.Core.Constants;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Service
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CommentDto>>> GetCommentsByBookId(int bookId)
        {
            var comments = await _unitOfWork.Comments.GetCommentsWithUserByBookId(bookId);

            var result = _mapper.Map<List<CommentDto>>(comments);

            return Response<IEnumerable<CommentDto>>.Success(200, result);
        }

        public async Task<Response<int>> PostComment(PostCommentDto postComment)
        {
            List<string> errors = new();

            if(postComment.UserId != null)
            {
                if(postComment.Content == null)
                {
                    errors.Add("Please enter your comment!");
                    return Response<int>.Failure(Messages.BAD_REQUEST, errors);
                }
            }
            else
            {
                if(postComment.AnonymousName == null)
                {
                    errors.Add("Please enter your name!");
                    return Response<int>.Failure(Messages.BAD_REQUEST, errors);
                }
                if (postComment.Content == null)
                {
                    errors.Add("Please enter your comment!");
                    return Response<int>.Failure(Messages.BAD_REQUEST, errors);
                }
            }

            var comment = _mapper.Map<Comment>(postComment);

            comment.IsActive = true;

            await _unitOfWork.Comments.AddAsync(comment);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.ADD_FAILURE);
                return Response<int>.Failure(Messages.SAVE_FAILURE, errors);
            }

            return Response<int>.Success(200, 1);
        }
    }
}
