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
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Navigation property for the cases assigned to the user
        /// </summary>
        public ICollection<ApplicationUserCase> AssignedCases { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public string? DepartmentId { get; set; }

        /// <summary>
        /// EF Core
        /// </summary>
        public Department? Department { get; set; } = null!;

        /// <summary>
        /// EF Core
        /// </summary>
        public ICollection<Case> CreatedCases { get; set; }

        /// <summary>
        /// EF Core
        /// </summary>
        public ICollection<Case> EditedCases { get; set; }

        /// <summary>
        /// EF Core
        /// </summary>
        public ICollection<CaseAction> CreatedCaseActions { get; set; }

        /// <summary>
        /// EF Core
        /// </summary>
        public ICollection<CaseAction> EditedCaseActions { get; set; }

        /// <summary>
        /// EF Core
        /// </summary>
        public ICollection<Report> CreatedReports { get; set; }

        /// <summary>
        /// EF Core
        /// </summary>
        public ICollection<Report> EditedReports { get; set; }

        /// <summary>
        /// Navigation ef core
        /// </summary>
        public ICollection<CaseEdit> CaseEdits { get; set; }

        public ICollection<Booking> CreatedBookings { get; set; }

        public ICollection<Release> CreatedReleases { get; set; }

        public ICollection<Charge> CreatedCharges { get; set; }

        public ICollection<Evidence> CreatedEvidence { get; set; }

        public ICollection<CaseNote> CreatedCaseNotes { get; set; }

        /// <summary>
        /// Initializes a new instance of the ApplicationUser class.
        /// </summary>
        public ApplicationUser() : base()
        {
        }
    }
}