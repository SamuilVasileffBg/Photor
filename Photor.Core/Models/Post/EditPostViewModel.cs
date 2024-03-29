﻿using System;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.Post;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Models.Post
{
    public class EditPostViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [StringLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string? Description { get; set; }

        public bool FriendsOnly { get; set; }
    }
}
