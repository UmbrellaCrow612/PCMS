using Microsoft.AspNetCore.Identity;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents an application user, extending the base IdentityUser class
    /// with additional properties specific to the PCMS application.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's rank.
        /// </summary>
        [PersonalData]
        public string Rank { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's badge number.
        /// </summary>
        [ProtectedPersonalData]
        public string BadgeNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        [ProtectedPersonalData]
        public DateTime DOB { get; set; }

        /// <summary>
        /// Navigation property for the cases assigned to the user
        /// </summary>
        public ICollection<ApplicationUserCase> AssignedUsers { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the ApplicationUser class.
        /// </summary>
        public ApplicationUser() : base()
        {
        }
    }
}