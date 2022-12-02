using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.Core.Services
{
    internal class SaveService : ISaveService
    {
        private readonly IRepository repository;

        public SaveService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> AddSaveAsync(Guid postId, string userId)
        {
            if (await FindSaveAsync(postId, userId) != null)
            {
                throw new Exception("Post already saved");
            }

            var userSavedPost = new UserSavedPost()
            {
                PostId = postId,
                UserId = userId,
                DateTime = DateTime.UtcNow,
                IsDeleted = false,
            };

            await repository
                .AddAsync<UserSavedPost>(userSavedPost);

            await repository
                .SaveChangesAsync();

            return userSavedPost.Id;
        }

        public async Task DeleteSaveAsync(Guid postId, string userId)
        {
            var userSavedPost = await FindSaveAsync(postId, userId);

            if (userSavedPost == null)
            {
                throw new Exception("Like not found");
            }

            userSavedPost.IsDeleted = true;

            await repository
                .SaveChangesAsync();
        }

        public async Task<UserSavedPost?> FindSaveAsync(Guid postId, string userId)
        {
            var userSavedPost = await repository
                .All<UserSavedPost>()
                .FirstOrDefaultAsync(ulp => ulp.UserId == userId && ulp.PostId == postId && ulp.IsDeleted == false);

            return userSavedPost;
        }
    }
}
