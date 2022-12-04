using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Post
{
    public class PostsPaginationViewModel
    {
        public string UserId { get; set; } = null!;

        public int AllPostsCount { get; set; }

        public int Page { get; set; }

        public IEnumerable<Photor.Infrastructure.Data.Models.Post> Posts { get; set; }
            = new List<Photor.Infrastructure.Data.Models.Post>();
    }
}
