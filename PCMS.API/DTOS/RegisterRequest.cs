﻿using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO (Data Transfer Object) for the user registration request.
    /// This excludes the AssignedCases property and is our own implementation based on ASP.Net Core Identity Routes Source Code.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// The email address of the user to be registered.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password of the user. Ensure this is securely hashed and not stored in plain text.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The user's first name.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The user's last name.
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The user's rank.
        /// </summary>
        [Required(ErrorMessage = "Rank is required.")]
        [StringLength(20, ErrorMessage = "Rank cannot exceed 20 characters.")]
        public string Rank { get; set; } = string.Empty;

        /// <summary>
        /// The badge number of the user.
        /// </summary>
        [Required(ErrorMessage = "Badge number is required.")]
        [StringLength(10, ErrorMessage = "Badge number cannot exceed 10 characters.")]
        public string BadgeNumber { get; set; } = string.Empty;

        /// <summary>
        /// The date of birth of the user.
        /// </summary>
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [CustomDateRange(ErrorMessage = "Date of birth must be a valid past date.")]
        public DateTime DOB { get; set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username cannot exceed 30 characters.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

    /// <summary>
    /// Custom validation for date range to ensure that the date is not in the future.
    /// </summary>
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute()
            : base(typeof(DateTime), DateTime.MinValue.ToShortDateString(), DateTime.Now.ToShortDateString())
        {
        }
    }
}
