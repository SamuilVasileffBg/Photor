using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models
{
    public class UserFriendViewModel
    {
        public string UserId { get; set; } = null!;

        public UserViewModel User { get; set; } = null!;

        public string FriendId { get; set; } = null!;

        public UserViewModel Friend { get; set; } = null!;
    }
}
