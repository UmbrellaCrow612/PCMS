namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a report in the system.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Gets the report ID, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        public string Id { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the report Title.
        /// </summary>
        public required string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the report CreatedById.
        /// </summary>
        public required string CreatedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the report LastEditedBy.
        /// </summary>
        public required string LastEditedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the report CreatedAt defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the report LastModifiedDate, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public required DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the report Details.
        /// </summary>
        public required string Details { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the report CaseId it is linked to <see cref="Case"/>.
        /// </summary>
        public required string CaseId { get; set; }

        /// <summary>
        /// Reference navigation property for <see cref="CaseId"/>.
        /// </summary>
        public Case? Case { get; set; } = null!;
    }
}