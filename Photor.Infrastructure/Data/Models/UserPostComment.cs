using System;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.UserPostComment;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Infrastructure.Data.Models
{
    public class UserPostComment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        [Required]
        [StringLength(CommentMaxLength), MinLength(CommentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        [DefaultValue("2000-01-01T01:01:01")]
        public DateTime DateTimeOfCreation { get; set; }

        public DateTime? DateTimeOfLastEdit { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
