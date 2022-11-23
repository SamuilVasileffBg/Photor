using Photor.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Friend
{
    public class FriendInvitationViewModel
    {
        public Guid Id;

        public UserViewModel Sender { get; set; } = null!;

        public UserViewModel Receiver { get; set; } = null!;
    }
}
