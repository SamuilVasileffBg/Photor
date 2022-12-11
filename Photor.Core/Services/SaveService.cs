using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;

namespace Photor.Core.Services
{
    public class SaveService : ISaveService
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
                throw new Exception("Save not found");
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

        //public async Task<IEnumerable<UserSavedPost>> GetSavedPostsAsync(string userId)
        //{
        //    return await repository
        //        .All<UserSavedPost>()
        //        .Where(usp => usp.IsDeleted == false && usp.UserId == userId)
        //        .Include(usp => usp.Post)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<UserSavedPost>> GetSavedPostsAsync(string userId, int page)
        {
            return await repository
                .All<UserSavedPost>()
                .Where(usp => usp.IsDeleted == false && usp.UserId == userId)
                .OrderByDescending(usp => usp.DateTime)
                .Skip((page - 1) * PostsPerPage)
                .Take(4)
                .Include(usp => usp.Post)
                .ToListAsync();
        }

        public async Task<int> GetSavedPostsCountAsync(string userId)
        {
            return await repository
                .All<UserSavedPost>()
                .Where(usp => usp.IsDeleted == false && usp.UserId == userId)
                .CountAsync();
        }
    }
}
