﻿using System.ComponentModel.DataAnnotations;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.ApplicationUser;

namespace Photor.Core.Models.User
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(UserNameMaxLength), MinLength(UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength)]
        public string FirstName { get; set; } = null!;

        [StringLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string? Description { get; set; }

        [Required]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength)]
        public string LastName { get; set; } = null!;

        public List<Photor.Infrastructure.Data.Models.Post>? Posts { get; set; }
    }
}
