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
    public class LikeService : ILikeService
    {
        private readonly IRepository repository;

        public LikeService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> AddLikeAsync(Guid postId, string userId)
        {
            if (await FindLikeAsync(postId, userId) != null)
            {
                throw new Exception("Post already liked");
            }

            var userLikedPost = new UserLikedPost()
            {
                PostId = postId,
                UserId = userId,
                DateTime = DateTime.UtcNow,
                IsDeleted = false,
            };

            await repository
                .AddAsync<UserLikedPost>(userLikedPost);

            await repository
                .SaveChangesAsync();

            return userLikedPost.Id;
        }

        public async Task DeleteLikeAsync(Guid postId, string userId)
        {
            var userLikedPost = await FindLikeAsync(postId, userId);

            if (userLikedPost == null)
            {
                throw new Exception("Like not found");
            }

            userLikedPost.IsDeleted = true;

            await repository
                .SaveChangesAsync();
        }

        public async Task<UserLikedPost?> FindLikeAsync(Guid postId, string userId)
        {
            var userLikedPost = await repository
                .All<UserLikedPost>()
                .FirstOrDefaultAsync(ulp => ulp.UserId == userId && ulp.PostId == postId && ulp.IsDeleted == false);

            return userLikedPost;
        }
    }
}
