using Microsoft.AspNetCore.Identity;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents an application user, extending the base IdentityUser class
    /// with additional properties specific to the PCMS application.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        [PersonalData]
        public string Rank { get; set; } = string.Empty;

        [ProtectedPersonalData]
        public string BadgeNumber { get; set; } = string.Empty;

        [ProtectedPersonalData]
        public DateTime DateOfBirth { get; set; }

        public ICollection<ApplicationUserCase> AssignedCases { get; set; } = [];

        public string? DepartmentId { get; set; }

        public Department? Department { get; set; } = null!;

        public ICollection<Case> CreatedCases { get; set; } = [];

        public ICollection<Case> EditedCases { get; set; } = [];

        public ICollection<CaseAction> CreatedCaseActions { get; set; } = [];

        public ICollection<CaseAction> EditedCaseActions { get; set; } = [];

        public ICollection<Report> CreatedReports { get; set; } = [];

        public ICollection<Report> EditedReports { get; set; } = [];

        public ICollection<CaseEdit> CaseEdits { get; set; } = [];

        public ICollection<Booking> CreatedBookings { get; set; } = [];

        public ICollection<Release> CreatedReleases { get; set; } = [];

        public ICollection<Charge> CreatedCharges { get; set; } = [];

        public ICollection<Evidence> CreatedEvidence { get; set; } = [];

        public ICollection<CaseNote> CreatedCaseNotes { get; set; } = [];

        public ICollection<Case> DeletedCases { get; set; } = [];

        public ICollection<Report> DeletedReports { get; set; } = [];

        public ICollection<Evidence> EditedEvidences { get; set; } = [];

        public ICollection<Evidence> DeletedEvidences { get; set; } = [];

        public ICollection<CaseNote> EditedCaseNotes { get; set; } = [];

        public ICollection<CaseNote> DeletedCaseNotes { get; set; } = [];

        public ICollection<Tag> CreatedTags { get; set; } = [];

        public ICollection<Tag> EditedTags { get; set; } = [];

        public ApplicationUser() : base()
        {
        }
    }
}