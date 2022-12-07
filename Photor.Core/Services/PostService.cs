using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;

namespace Photor.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;
        private readonly IFriendService friendService;
        private readonly IGoogleDriveService googleDriveService;

        public PostService(ApplicationDbContext context, IRepository repository, IFriendService friendService, IGoogleDriveService googleDriveService)
        {
            this.context = context;
            this.repository = repository;
            this.friendService = friendService;
            this.googleDriveService = googleDriveService;
        }

        public async Task<bool> Accessible(Post post, string userId)
        {
            if (post.FriendsOnly == false)
            {
                return true;
            }

            if (post.UserId == userId)
            {
                return true;
            }

            if ((await friendService.FindUserFriendAsync(post.UserId, userId)) == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Guid> AddPostAsync(AddPostViewModel model)
        {
            var imageUrl = await googleDriveService.UploadImageAsync(model.Image);

            Post post = new Post()
            {
                Description = model.Description,
                FriendsOnly = model.FriendsOnly,
                IsDeleted = false,
                ImageUrl = imageUrl,
                UserId = model.UserId,
                DateTimeOfCreation = DateTime.UtcNow
            };

            await repository
                .AddAsync(post);

            await repository
                .SaveChangesAsync();

            return post.Id;
        }

        public async Task DeletePostAsync(Guid id)
        {
            var post = await GetPostAsync(id.ToString());

            if (post == null)
            {
                throw new Exception("Cannot find such post.");
            }

            foreach (var like in post.PostLikes)
            {
                like.IsDeleted = true;
            }

            foreach (var comment in post.PostComments)
            {
                comment.IsDeleted = true;
            }

            foreach (var save in post.PostSaves)
            {
                save.IsDeleted = true;
            }

            foreach (var report in post.PostReports)
            {
                report.IsDeleted = true;
            }

            post.IsDeleted = true;

            await repository
                .SaveChangesAsync();
        }

        public async Task EditPostAsync(EditPostViewModel model)
        {
            var post = await repository
                .All<Post>()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (post == null)
            {
                throw new Exception("Cannot find such post.");
            }

            post.Description = model.Description;
            post.FriendsOnly = model.FriendsOnly;
            post.DateTimeOfLastEdit = DateTime.UtcNow;

            await repository
                .SaveChangesAsync();
        }

        public async Task<Post?> GetPostAsync(string id)
        {
            return await repository
                .All<Post>()
                .Include(p => p.ApplicationUser)
                .Include(p => p.PostLikes)
                .Include(p => p.PostComments)
                .Include(p => p.PostSaves)
                .Include(p => p.PostReports)
                .FirstOrDefaultAsync(p => p.Id.ToString() == id && p.IsDeleted == false);
        }

        public async Task<List<Post>> GetUserPostsAsync(string userId)
        {
            return await repository
                .All<Post>()
                .Where(p => p.UserId == userId && p.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Post>> GetUserPostsAsync(string userId, int page)
        {
            return await repository
                .All<Post>()
                .Where(p => p.UserId == userId && p.IsDeleted == false)
                .OrderByDescending(p => p.DateTimeOfCreation)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToListAsync();
        }

        public async Task<int> GetUserPostsCountAsync(string userId)
        {
            return await repository
                .All<Post>()
                .Where(p => p.UserId == userId && p.IsDeleted == false)
                .CountAsync();
        }
    }
}
