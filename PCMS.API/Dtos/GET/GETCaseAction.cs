using PCMS.API.Models;

namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO for GET a case action
    /// </summary>
    public record GETCaseAction
    {
        /// <summary>
        /// Gets or sets the case action Id.
        /// </summary>
        public required string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action name.
        /// </summary>
        public required string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action description.
        /// </summary>
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action type.
        /// </summary>
        public required string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action created at time, defaults to now.
        /// </summary>
        public required DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the case action last modified at.
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the case action creator.
        /// </summary>
        public required GETApplicationUser Creator { get; set; }

        /// <summary>
        /// Gets or sets the case action last edited by user.
        /// </summary>
        public GETApplicationUser? LastEditor { get; set; }

        /// <summary>
        /// Gets or sets the case action CaseId.
        /// </summary>
        public required string CaseId { get; set; } = string.Empty;

    }
}