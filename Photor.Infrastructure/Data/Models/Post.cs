using System;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.Post;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Infrastructure.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string UserId { get; set; } = null!;

        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [StringLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string? Description { get; set; }

        //photo

        //comments

        [Required]
        public bool FriendsOnly { get; set; }

        [Required]
        [DefaultValue("")]
        public DateTime DateTimeOfCreation { get; set; }

        public DateTime? DateTimeOfLastEdit { get; set; }

        public List<UserLikedPost> PostLikes { get; set; } = new List<UserLikedPost>();

        public List<UserPostComment> PostComments { get; set; } = new List<UserPostComment>();

        public List<UserSavedPost> PostSaves { get; set; } = new List<UserSavedPost>();

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
