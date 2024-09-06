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
        /// Gets or sets the list of cases assigned to the user.
        /// </summary>
        public List<Case> AssignedCases { get; set; } = new List<Case>();

        /// <summary>
        /// Gets the full name of the user.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Initializes a new instance of the ApplicationUser class.
        /// </summary>
        public ApplicationUser() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ApplicationUser class with a specified user name.
        /// </summary>
        /// <param name="userName">The user name for the new user.</param>
        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}