using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO for the user registration request.
    /// This excludes the AssignedCases property and is our own implementation based on ASP.Net Core Identity Routes Source Code.
    /// </summary>
    public record CreateRegisterRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s-']+$", ErrorMessage = "First name can only contain letters, spaces, hyphens and apostrophes.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s-']+$", ErrorMessage = "Last name can only contain letters, spaces, hyphens and apostrophes.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Rank is required.")]
        [StringLength(20, ErrorMessage = "Rank cannot exceed 20 characters.")]
        public required string Rank { get; set; }


        [Required(ErrorMessage = "Badge number is required.")]
        [StringLength(10, ErrorMessage = "Badge number cannot exceed 10 characters.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Badge number can only contain uppercase letters and numbers.")]
        public required string BadgeNumber { get; set; }


        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [NotInFuture(ErrorMessage = "Date of birth must be a valid past date.")]
        public required DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Username can only contain letters, numbers, underscores and hyphens.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public required string Role { get; set; }
    }
}