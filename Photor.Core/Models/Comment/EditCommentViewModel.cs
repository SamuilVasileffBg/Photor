using System;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.UserPostComment;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Photor.Core.Models.Comment
{
    public class EditCommentViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid PostId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [StringLength(CommentMaxLength), MinLength(CommentMinLength)]
        public string Content { get; set; } = null!;
    }
}
