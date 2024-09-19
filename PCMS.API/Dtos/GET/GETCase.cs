using PCMS.API.Models;

namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO for GET a case object
    /// </summary>
    public record GETCase
    {
        /// <summary>
        /// Gets or sets the Case Id.
        /// </summary>
        public required string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case Number.
        /// </summary>
        public required string CaseNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case title.
        /// </summary>
        public required string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case description.
        /// </summary>
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case status based on the <see cref="CaseStatus"/> enum.
        /// </summary>
        public required CaseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the time the case was opened defaults to now.
        /// </summary>
        public required DateTime DateOpened { get; set; }

        /// <summary>
        /// Gets or sets the time the case was closed defaults to null.
        /// </summary>
        public required DateTime? DateClosed { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the case was last modified.
        /// </summary>
        public required DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the case priority based on the <see cref="CasePriority"/> enum.
        /// </summary>
        public required CasePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the case type.
        /// </summary>
        public required string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user Id Who created the case.
        /// </summary>
        public required string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the user Id who last modified the case.
        /// </summary>
        public required string LastEditedById { get; set; }

    }
}