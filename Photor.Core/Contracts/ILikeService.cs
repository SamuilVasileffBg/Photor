using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface ILikeService
    {
        public Task<Guid> AddLikeAsync(Guid postId, string userId);

        public Task DeleteLikeAsync(Guid postId, string userId);

        public Task<UserLikedPost?> FindLikeAsync(Guid postId, string userId);

        public Task<int> GetPostLikesCountAsync(Guid postId);
    }
}
