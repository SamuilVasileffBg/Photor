﻿using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface ICommentService
    {
        public Task<Guid> AddCommentAsync(Guid postId, string userId, string commentContent);

        public Task DeleteTaskAsync(Guid postId, string userId);

        public Task<List<UserPostComment>> GetPostCommentsAsync(Guid postId);
    }
}
