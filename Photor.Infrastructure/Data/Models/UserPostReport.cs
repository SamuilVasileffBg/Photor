﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.UserPostReport;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Photor.Infrastructure.Data.Models
{
    public class UserPostReport
    {
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
        [StringLength(ReasonMaxLength), MinLength(ReasonMinLength)]
        public string Reason { get; set; } = null!;

        [Required]
        [DefaultValue("")]
        public DateTime DateTime { get; set; }

        [Required]
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
