namespace PCMS.API.DTOS
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
        public required DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the case action created by Id.
        /// </summary>
        public required string CreatedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action last edited by Id.
        /// </summary>
        public required string LastEditedById { get; set; } = string.Empty;

    }
}