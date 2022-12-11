using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface ISaveService
    {
        public Task<Guid> AddSaveAsync(Guid postId, string userId);

        public Task DeleteSaveAsync(Guid postId, string userId);

        public Task<UserSavedPost?> FindSaveAsync(Guid postId, string userId);

        //public Task<IEnumerable<UserSavedPost>> GetSavedPostsAsync(string userId);

        public Task<IEnumerable<UserSavedPost>> GetSavedPostsAsync(string userId, int page);

        public Task<int> GetSavedPostsCountAsync(string userId);
    }
}
