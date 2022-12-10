using Photor.Core.Models.User;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Friend
{
    public class FriendInvitationListViewModel
    {
        public string UserId { get; set; } = null!;

        public int? Page { get; set; }

        public int AllInvitationsCount { get; set; }

        public IEnumerable<FriendInvitation> Invitations { get; set; } = new List<FriendInvitation>();
    }
}
