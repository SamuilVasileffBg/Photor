﻿using Photor.Core.Models;
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

        public Task<Post> GetPostAsync(string id);
    }
}
