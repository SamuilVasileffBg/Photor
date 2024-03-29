﻿using System.ComponentModel.DataAnnotations;

namespace Photor.Core.Models.Identity
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
