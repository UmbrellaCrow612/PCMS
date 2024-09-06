using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO (Data Transfer Object) for the user registration request.
    /// This excludes the AssignedCases property. And Is our own implamentation of it based on ASP.Net Core Identity Routes Source Code
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// The email address of the user to be registered.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password of the user. Ensure this is securely hashed and not stored in plain text.
        /// </summary>
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The user's first name.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The user's last name.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The user's rank.
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "Rank cannot exceed 20 characters.")]
        public string Rank { get; set; } = string.Empty;

        /// <summary>
        /// The badge number of the user.
        /// </summary>
        [Required]
        [StringLength(10, ErrorMessage = "Badge number cannot exceed 10 characters.")]
        public string BadgeNumber { get; set; } = string.Empty;

        /// <summary>
        /// The date of birth of the user.
        /// </summary>
        [Required]
        public DateTime DOB { get; set; }

        /// <summary>
        /// The username of the user
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the user
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
