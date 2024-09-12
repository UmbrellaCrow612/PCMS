namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a specific action taken on a case.
    /// </summary>
    public class CaseAction
    {
        /// <summary>
        /// Gets or sets the case action Id. Defaults to <see cref="Guid.NewGuid()"/>
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

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
        /// Gets or sets the case action created at time, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the case action last modified at, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the case action created by Id.
        /// </summary>
        public required string CreatedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action last edited by Id.
        /// </summary>
        public required string LastEditedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the specific case Id this case action is linked to, required.
        /// </summary>
        public required string CaseId { get; set; }

        /// <summary>
        /// Gets or sets the specific case this case action is linked to, required.
        /// </summary>
        public Case? Case { get; set; } = null!;
    }
}
