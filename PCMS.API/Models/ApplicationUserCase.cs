namespace PCMS.API.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between ApplicationUser and Case.
    /// </summary>
    public class ApplicationUserCase
    {
        /// <summary>
        /// Get or set the app user ID.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// EF core nav property.
        /// </summary>
        public ApplicationUser? ApplicationUser { get; set; } = null!;

        /// <summary>
        /// Get or set the case ID.
        /// </summary>
        public required string CaseId { get; set; }

        /// <summary>
        /// EF core nav property.
        /// </summary>
        public Case? Case { get; set; } = null!;
    }
}
