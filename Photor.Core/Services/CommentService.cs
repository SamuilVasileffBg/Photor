using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            };

            await repository
                .AddAsync<UserPostComment>(comment);

            await repository
                .SaveChangesAsync();

            return comment.Id;
        }

        public Task DeleteTaskAsync(Guid postId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserPostComment>> GetPostCommentsAsync(Guid postId)
        {
            return await repository
                .All<UserPostComment>()
                .Where(upc => upc.PostId == postId)
                .Include(upc => upc.User)
                .ToListAsync();
        }
    }
}
