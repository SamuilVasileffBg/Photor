using Photor.Core.Models.Post;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface IPostService
    {
        public Task<Guid> AddPostAsync(AddPostViewModel model);

        public Task<Post?> GetPostAsync(string id);

        public Task EditPostAsync(EditPostViewModel model);

        public Task DeletePostAsync(Guid id);
    }
}
