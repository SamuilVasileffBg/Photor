using Photor.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Comment
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public UserViewModel User { get; set; }
    }
}
