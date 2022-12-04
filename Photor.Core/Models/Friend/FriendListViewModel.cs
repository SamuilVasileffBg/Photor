using Photor.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Friend
{
    
    public class FriendListViewModel
    {
        public string UserId { get; set; } = null!;

        public int? Page { get; set; }

        public int AllFriendsCount { get; set; }

        public IEnumerable<UserViewModel> Friends { get; set; } = new List<UserViewModel>();
    }
}
