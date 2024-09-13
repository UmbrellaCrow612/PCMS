namespace PCMS.API.Models
{
    /// <summary>
    /// Represents evidence linked to a case.
    /// </summary>
    public class Evidence
    {
        /// <summary>
        /// Gets the evidence ID, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        public string Id { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the evidence CaseId it is linked to, <see cref="Case"/>.
        /// </summary>
        public required string CaseId { get; set; } = string.Empty;

        /// <summary>
        /// EF Core navigation property, linked to <see cref="CaseId"/>.
        /// </summary>
        public Case? Case { get; set; } = null!;

        /// <summary>
        /// Gets or sets the file url of evidence.
        /// </summary>
        public required string FileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of evidence (e.g., Physical, Digital, Documentary).
        /// </summary>
        public required string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the evidence.
        /// </summary>
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location where the evidence is stored.
        /// </summary>
        public required string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the evidence was collected.
        /// </summary>
        public required DateTime CollectionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the evidence entry.
        /// </summary>
        public required string CreatedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the evidence LastEditedBy.
        /// </summary>
        public required string LastEditedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the person details of who collected the evidence.
        /// </summary>
        public required string CollectedByDetails { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the evidence LastModifiedDate, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public required DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the evidence CreatedAt defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}