﻿using Microsoft.EntityFrameworkCore;
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
    }
}
