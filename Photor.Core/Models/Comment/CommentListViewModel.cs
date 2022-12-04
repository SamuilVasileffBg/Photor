using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Comment
{
    public class CommentListViewModel
    {
        public Guid PostId { get; set; }

        public int? Page { get; set; }

        public int AllCommentsCount { get; set; }

        public IEnumerable<UserPostComment> Comments { get; set; } = new List<UserPostComment>();
    }
}
