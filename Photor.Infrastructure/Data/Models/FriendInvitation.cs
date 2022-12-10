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
    public class FriendInvitation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string SenderId { get; set; } = null!;

        public ApplicationUser Sender { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;

        public ApplicationUser Receiver { get; set; } = null!;

        [Required]
        [DefaultValue("")]
        public DateTime DateTime { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
