using System.ComponentModel.DataAnnotations;
using static Photor.Infrastructure.Data.Constants.DbModelsConstants.ApplicationUser;

namespace Photor.Core.Models.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(FirstAndLastNameMaxLength), MinLength(FirstAndLastNameMinLength)]
        public string LastName { get; set; } = null!;
    }
}
