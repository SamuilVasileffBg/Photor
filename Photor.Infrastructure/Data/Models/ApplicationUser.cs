using Microsoft.AspNetCore.Identity;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.ApplicationUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Photor.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DefaultValue("FirstName")]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [DefaultValue("LastName")]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength)]
        public string LastName { get; set; } = null!;

        [StringLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string? Description { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<UserPostComment> UserPostComments { get; set; } = new List<UserPostComment>();

        public List<UserPostReport> UserPostReports { get; set; } = new List<UserPostReport>();

        public List<UserLikedPost> UserLikedPosts { get; set; } = new List<UserLikedPost>();


        [InverseProperty(nameof(UserFriend.User))]
        public List<UserFriend> UsersFriends { get; set; } = new List<UserFriend>();

        [InverseProperty(nameof(UserFriend.Friend))]
        public List<UserFriend> UsersFriendsTo { get; set; } = new List<UserFriend>();

        [InverseProperty(nameof(FriendInvitation.Sender))]
        public List<FriendInvitation> FriendInvitationsSent { get; set; } = new List<FriendInvitation>();

        public List<UserSavedPost> UserSavedPosts { get; set; } = new List<UserSavedPost>();

        [Required]
        [DefaultValue(@"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey")]
        public string ImageUrl { get; set; } = null!;
    }
}
