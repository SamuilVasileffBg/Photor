using System;
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

        [StringLength(200)]
        [Column(TypeName = "NVARCHAR(200)")]
        public string? Description { get; set; }

        //photo

        //comments

        [Required]
        public bool FriendsOnly { get; set; }

        public List<UserLikedPost> PostLikedUsers { get; set; } = new List<UserLikedPost>();

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
