using Photor.Core.Models.Friend;
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

        //public Task<IEnumerable<FriendInvitationViewModel>> GetReceivedFriendInvitationsAsync(string receiverId);

        public Task<IEnumerable<FriendInvitation>> GetReceivedFriendInvitationsAsync(string receiverId, int page);

        public Task<int> GetReceivedFriendInvitationsCountAsync(string receiverId);

        public Task<FriendInvitation?> FindFriendInvitationAsync(string senderId, string receiverId);

        public Task<UserFriend?> FindUserFriendAsync(string id1, string id2);

        public UserFriend? FindUserFriend(string id1, string id2);

        public Task AcceptFriendInvitationAsync(string senderId, string receiverId);

        public Task RejectFriendInvitationAsync(string senderId, string receiverId);

        public Task AddUserFriendAsync(string userId, string friendId);

        public Task RemoveUserFriendAsync(string userId, string friendId);

        public IQueryable<ApplicationUser> GetUserFriendsAsync(string userId);

        public Task<IEnumerable<ApplicationUser>> GetUserFriendsAsync(string userId, int page);

        public Task<int> GetUserFriendsCountAsync(string userId);

    }
}
