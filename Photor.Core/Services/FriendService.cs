using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.Friend;
using Photor.Core.Parsers;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Services
{
    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext context;
        private readonly IUserService userService;

        public FriendService(ApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task AcceptFriendInvitationAsync(string senderId, string receiverId)
        {
            var friendInvitation = await FindFriendInvitationAsync(senderId, receiverId);

            if (friendInvitation == null)
            {
                throw new NullReferenceException("There is no existing invitation");
            }

            friendInvitation.IsDeleted = true;

            await AddUserFriendAsync(senderId, receiverId);

            await context.SaveChangesAsync();
        }
        public async Task RejectFriendInvitationAsync(string senderId, string receiverId)
        {
            var friendInvitation = await FindFriendInvitationAsync(senderId, receiverId);

            if (friendInvitation == null)
            {
                throw new NullReferenceException("There is no existing invitation");
            }

            friendInvitation.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task<FriendInvitation?> FindFriendInvitationAsync(string senderId, string receiverId)
        {
            var invitation = await context
                .FriendInvitations
                .Where(fi => fi.IsDeleted == false)
                .FirstOrDefaultAsync(f => f.SenderId == senderId && f.ReceiverId == receiverId);

            return invitation;
        }

        public async Task<UserFriend?> FindUserFriendAsync(string id1, string id2)
        {
            var userFriend = await context
                .UsersFriends
                .Where(uf => uf.IsDeleted == false)
                .FirstOrDefaultAsync(f => (f.UserId == id1 && f.FriendId == id2)
                                       || (f.UserId == id2 && f.FriendId == id1));

            return userFriend;
        }

        public async Task<IEnumerable<FriendInvitationViewModel>> GetReceivedFriendInvitationsAsync(string receiverId)
        {
            if (await context.Users.FirstOrDefaultAsync(u => u.Id == receiverId) == null)
            {
                throw new ArgumentException(nameof(receiverId));
            }

            var data = await context
                .FriendInvitations
                .Where(fi => fi.ReceiverId == receiverId && fi.IsDeleted == false)
                .Select(fi => new FriendInvitationViewModel
                {
                    Id = fi.Id,
                    Receiver = fi.Receiver.ParseToViewModel(),
                    Sender = fi.Sender.ParseToViewModel(),
                })
                .ToListAsync();

            return data;
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

            if (await context.Users.FirstOrDefaultAsync(u => u.Id == receiverId) == null)
            {
                throw new ArgumentNullException(nameof(receiverId));
            }

            if (await context.Users.FirstOrDefaultAsync(u => u.Id == senderId) == null)
            {
                throw new ArgumentNullException(nameof(senderId));
            }

            await context
                .FriendInvitations
                .AddAsync(new FriendInvitation()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                });

            await context.SaveChangesAsync();

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
                throw new Exception("There is an already existing friendship between these users.");
            }

            var userFriend = await context
                .UsersFriends
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

            context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUserFriendsAsync(string userId)
        {
            //var userFriends = context
            //    .UsersFriends
            //    .Include(uf => uf.User)
            //    .Include(uf => uf.Friend)
            //    .Where(uf => uf.IsDeleted == false)
            //    .ToList();
            //
            //userFriends = userFriends.Where(uf => uf.UserId == userId || uf.FriendId == userId).ToList();
            //
            //var userCollection = new List<ApplicationUser>();
            //
            //foreach (var userFriend in userFriends)
            //{
            //    if (userFriend.UserId == userId)
            //    {
            //        userCollection.Add(userFriend.Friend);
            //    }
            //    else
            //    {
            //        userCollection.Add(userFriend.User);
            //    }
            //}

            return await context
                .UsersFriends
                .Include(uf => uf.User)
                .Include(uf => uf.Friend)
                .Where(uf => uf.IsDeleted == false)
                .Where(uf => uf.UserId == userId || uf.FriendId == userId)
                .Select(uf => uf.UserId == userId ? uf.Friend
                : uf.User)
                .ToListAsync();
        }

        public async Task DeleteFriendInvitationAsync(string senderId, string receiverId)
        {
            var friendInvitation = await FindFriendInvitationAsync(senderId, receiverId);

            if (friendInvitation == null)
            {
                throw new Exception("Cannot delete an unexisting friend invitation.");
            }

            friendInvitation.IsDeleted = true;

            await context.SaveChangesAsync();
        }
    }
}
