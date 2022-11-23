using Photor.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Post
{
    public class ViewPostViewModel
    {
        public Guid Id { get; set; }

        public UserViewModel User { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string? Description { get; set; }
    }
}
