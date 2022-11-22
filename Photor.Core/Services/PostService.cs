using Photor.Core.Contracts;
using Photor.Core.Models;
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
                FriendsOnly = false,
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

        public async Task<Post> GetPostAsync(string id)
        {
            return await repository
                .GetByIdAsync<Post>(id);
        }
    }
}
