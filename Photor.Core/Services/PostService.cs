using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public PostService(ApplicationDbContext context, IRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        public async Task<Guid> AddPostAsync(AddPostViewModel model)
        {
            Post post = new Post()
            {
                Description = model.Description,
                FriendsOnly = model.FriendsOnly,
                IsDeleted = false,
                ImageUrl = model.ImageUrl,
                UserId = model.UserId,
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

            await repository
                .SaveChangesAsync();
        }

        public async Task<Post?> GetPostAsync(string id)
        {
            return await repository
                .All<Post>()
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.Id.ToString() == id && p.IsDeleted == false);
        }
    }
}
