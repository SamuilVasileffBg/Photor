using Photor.Core.Models;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface IFriendService
    {
        public Task<bool> SendFriendInvitationAsync(string senderId, string receiverId);

        public Task DeleteFriendInvitationAsync(string senderId, string receiverId);

        public Task<IEnumerable<FriendInvitationViewModel>> GetReceivedFriendInvitationsAsync(string receiverId);

        public Task<FriendInvitation> FindFriendInvitationAsync(string senderId, string receiverId);

        public Task<UserFriend> FindUserFriendAsync(string id1, string id2);

        public Task AcceptFriendInvitationAsync(string senderId, string receiverId);

        public Task RejectFriendInvitationAsync(string senderId, string receiverId);

        public Task AddUserFriendAsync(string userId, string friendId);

        public Task RemoveUserFriendAsync(string userId, string friendId);

        public Task<IEnumerable<ApplicationUser>> GetUserFriendsAsync(string userId);

    }
}
