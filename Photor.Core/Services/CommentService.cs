using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.Comment;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;

        public CommentService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> AddCommentAsync(Guid postId, string userId, string commentContent)
        {
            var comment = new UserPostComment()
            {
                PostId = postId,
                UserId = userId,
                Content = commentContent,
                DateTimeOfCreation = DateTime.UtcNow,
                DateTimeOfLastEdit = null,
            };

            await repository
                .AddAsync<UserPostComment>(comment);

            await repository
                .SaveChangesAsync();

            return comment.Id;
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var comment = await GetCommentAsync(commentId);

            if (comment == null)
            {
                throw new Exception("Comment not found");
            }

            comment.IsDeleted = true;

            await repository
                .SaveChangesAsync();
        }

        public async Task<List<UserPostComment>> GetPostCommentsAsync(Guid postId)
        {
            return await repository
                .All<UserPostComment>()
                .Where(upc => upc.PostId == postId && upc.IsDeleted == false)
                .Include(upc => upc.User)
                .Include(upc => upc.Post)
                .ToListAsync();
        }

        public async Task<UserPostComment?> GetCommentAsync(Guid commentId)
        {
            return await repository
                .All<UserPostComment>()
                .Include(upc => upc.User)
                .Include(upc => upc.Post)
                .FirstOrDefaultAsync(upc => upc.Id == commentId && upc.IsDeleted == false);
        }

        public async Task EditCommentAsync(EditCommentViewModel model)
        {
            var comment = await GetCommentAsync(model.Id);

            if (comment == null)
            {
                throw new Exception("Comment not found.");
            }

            comment.Content = model.Content;
            comment.DateTimeOfLastEdit = DateTime.UtcNow;

            await repository
                .SaveChangesAsync();
        }

        public async Task<List<UserPostComment>> GetPostCommentsAsync(Guid postId, int page)
        {
            return await repository
                .All<UserPostComment>()
                .Where(upc => upc.PostId == postId && upc.IsDeleted == false)
                .OrderByDescending(upc => upc.DateTimeOfCreation)
                .Skip((page - 1) * 5)
                .Take(5)
                .Include(upc => upc.User)
                .Include(upc => upc.Post)
                .ToListAsync();
        }

        public async Task<int> GetPostCommentsCountAsync(Guid postId)
        {
            return await repository
                .All<UserPostComment>()
                .Where(upc => upc.PostId == postId && upc.IsDeleted == false)
                .CountAsync();
        }
    }
}
