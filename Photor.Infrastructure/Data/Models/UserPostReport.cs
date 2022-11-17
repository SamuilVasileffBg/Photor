using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Infrastructure.Data.Models
{
    public class UserPostReport
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = new ApplicationUser();

        [Required]
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public Post Post { get; set; } = new Post();

        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        public string Description { get; set; } = null!;
    }
}
