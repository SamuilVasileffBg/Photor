using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models
{
    public class FriendInvitationViewModel
    {
        public Guid Id;

        public UserViewModel Sender { get; set; } = null!;

        public UserViewModel Receiver { get; set; } = null!;
    }
}
