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
    public class UserFriend
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        [Required]
        public string FriendId { get; set; } = null!;

        public ApplicationUser Friend { get; set; } = null!;

        [Required]
        [DefaultValue("")]
        public DateTime DateTime { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
