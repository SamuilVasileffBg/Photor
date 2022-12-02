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
    public class UserSavedPost
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;

        [Required]
        public DateTime DateTime { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
