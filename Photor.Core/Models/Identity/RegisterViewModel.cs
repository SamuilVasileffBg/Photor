using System.ComponentModel.DataAnnotations;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.ApplicationUser;

namespace Photor.Core.Models.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength), MinLength(UserNameMinLength, ErrorMessage = "Email should be at least 5 digits long.")]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(EmailMaxLength), MinLength(EmailMinLength, ErrorMessage = "Email should be at least 10 digits long.")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength), MinLength(PasswordMinLength, ErrorMessage = "Password should be at least 5 digits long.")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength, ErrorMessage = "First name should be at least 2 digits long.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength, ErrorMessage = "Last name should be at least 2 digits long.")]
        public string LastName { get; set; } = null!;
    }
}
