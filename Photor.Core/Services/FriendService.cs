using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.Friend;
using Photor.Core.Parsers;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;

namespace Photor.Core.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUserService userService;
        private readonly IRepository repository;

        public FriendService(IUserService userService, IRepository repository)
        {
            this.userService = userService;
            this.repository = repository;
        }

        public async Task AcceptFriendInvitationAsync(string senderId, string receiverId)
        {
            var friendInvitation = await FindFriendInvitationAsync(senderId, receiverId);

            if (friendInvitation == null)
            {
                throw new Exception("There is no existing invitation");
            }

            friendInvitation.IsDeleted = true;

            await AddUserFriendAsync(senderId, receiverId);

            await repository.SaveChangesAsync();
        }
        public async Task RejectFriendInvitationAsync(string senderId, string receiverId)
        {
            var friendInvitation = await FindFriendInvitationAsync(senderId, receiverId);

            if (friendInvitation == null)
            {
                throw new Exception("There is no existing invitation");
            }

            friendInvitation.IsDeleted = true;
            await repository.SaveChangesAsync();
        }

        public async Task<FriendInvitation?> FindFriendInvitationAsync(string senderId, string receiverId)
        {
            var invitation = await repository
                .All<FriendInvitation>()
                .Where(fi => fi.IsDeleted == false)
                .FirstOrDefaultAsync(f => f.SenderId == senderId && f.ReceiverId == receiverId);

            return invitation;
        }

        public async Task<UserFriend?> FindUserFriendAsync(string id1, string id2)
        {
            var userFriend = await repository
                .All<UserFriend>()
                .Where(uf => uf.IsDeleted == false)
                .FirstOrDefaultAsync(f => (f.UserId == id1 && f.FriendId == id2)
                                       || (f.UserId == id2 && f.FriendId == id1));

            return userFriend;
        }

        public async Task<IEnumerable<FriendInvitation>> GetReceivedFriendInvitationsAsync(string receiverId, int page)
        {
            var data = await repository
                .All<FriendInvitation>()
                .Where(fi => fi.ReceiverId == receiverId && fi.IsDeleted == false)
                .OrderBy(fi => fi.DateTime)
                .Skip((page - 1) * FriendsPerPage)
                .Take(FriendsPerPage)
                .Include(i => i.Sender)
                .Include(i => i.Receiver)
                .ToListAsync();

            return data;
        }

        public async Task<int> GetReceivedFriendInvitationsCountAsync(string receiverId)
        {
            var count = await repository
               .All<FriendInvitation>()
               .Where(fi => fi.ReceiverId == receiverId && fi.IsDeleted == false)
               .CountAsync();

            return count;
        }


        public async Task<bool> SendFriendInvitationAsync(string senderId, string receiverId)
        {
            if (senderId == receiverId)
            {
                throw new ArgumentException("Cannot send a friend invitation to yourself.");
            }
            
            if (await FindUserFriendAsync(senderId, receiverId) != null)
            {
                throw new ArgumentException("There is an already existing friendship between these users.");
            }

            if (await FindFriendInvitationAsync(senderId, receiverId) != null)
            {
                throw new ArgumentException("There is already an existing invitation.");
            }

            if (await FindFriendInvitationAsync(receiverId, senderId) != null)
            {
                throw new ArgumentException("There is already an existing invitation.");
            }

            if (await repository.All<ApplicationUser>().FirstOrDefaultAsync(u => u.Id == receiverId) == null)
            {
                throw new ArgumentNullException(nameof(receiverId));
            }

            if (await repository.All<ApplicationUser>().FirstOrDefaultAsync(u => u.Id == senderId) == null)
            {
                throw new ArgumentNullException(nameof(senderId));
            }

            await repository
                .AddAsync(new FriendInvitation()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    DateTime = DateTime.UtcNow,
                });

            await repository.SaveChangesAsync();

            return true;
        }

        public async Task AddUserFriendAsync(string userId, string friendId)
        {
            if (userId == friendId)
            {
                throw new ArgumentException("Cannot add yourself as a friend.");
            }

            if (await FindUserFriendAsync(userId, friendId) != null)
            {
                throw new ArgumentException("There is an already existing friendship between these users.");
            }

            await repository
                .AddAsync(new UserFriend()
                {
                    UserId = userId,
                    FriendId = friendId,
                });
        }

        public async Task RemoveUserFriendAsync(string userId, string friendId)
        {
            var userFriend = await FindUserFriendAsync(userId, friendId);

            if (userFriend == null)
            {
                throw new Exception("There is no existing friendship between these users.");
            }

            userFriend.IsDeleted = true;

            await repository.SaveChangesAsync();
        }

        //public async Task<IEnumerable<ApplicationUser>> GetUserFriendsAsync(string userId)
        //{
        //    //var userFriends = context
        //    //    .UsersFriends
        //    //    .Include(uf => uf.User)
        //    //    .Include(uf => uf.Friend)
        //    //    .Where(uf => uf.IsDeleted == false)
        //    //    .ToList();
        //    //
        //    //userFriends = userFriends.Where(uf => uf.UserId == userId || uf.FriendId == userId).ToList();
        //    //
        //    //var userCollection = new List<ApplicationUser>();
        //    //
        //    //foreach (var userFriend in userFriends)
        //    //{
        //    //    if (userFriend.UserId == userId)
        //    //    {
        //    //        userCollection.Add(userFriend.Friend);
        //    //    }
        //    //    else
        //    //    {
        //    //        userCollection.Add(userFriend.User);
        //    //    }
        //    //}

        //    return await repository
        //        .All<UserFriend>()
        //        .Include(uf => uf.User)
        //        .Include(uf => uf.Friend)
        //        .Where(uf => uf.IsDeleted == false)
        //        .Where(uf => uf.UserId == userId || uf.FriendId == userId)
        //        .Select(uf => uf.UserId == userId ? uf.Friend
        //        : uf.User)
        //        .ToListAsync();
        //}

        public async Task DeleteFriendInvitationAsync(string senderId, string receiverId)
        {
            var friendInvitation = await FindFriendInvitationAsync(senderId, receiverId);

            if (friendInvitation == null)
            {
                throw new Exception("Cannot delete an unexisting friend invitation.");
            }

            friendInvitation.IsDeleted = true;

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUserFriendsAsync(string userId, int page)
        {
            return await repository
                .All<UserFriend>()
                .Include(uf => uf.User)
                .Include(uf => uf.Friend)
                .Where(uf => uf.IsDeleted == false)
                .Where(uf => uf.UserId == userId || uf.FriendId == userId)
                .OrderBy(uf => uf.UserId == userId ? uf.FriendId : uf.UserId)
                .Skip((page - 1) * 5)
                .Take(5)
                .Select(uf => uf.UserId == userId ? uf.Friend
                : uf.User)
                .ToListAsync();
        }

        public async Task<int> GetUserFriendsCountAsync(string userId)
        {
            return await repository
                .All<UserFriend>()
                .Include(uf => uf.User)
                .Include(uf => uf.Friend)
                .Where(uf => uf.IsDeleted == false)
                .Where(uf => uf.UserId == userId || uf.FriendId == userId)
                .CountAsync();
        }
    }
}
