namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a specific action taken on a case.
    /// </summary>
    public class CaseAction
    {
        /// <summary>
        /// Gets or sets the case action Id.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the case action name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action created at time, defaults to now.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the specific case Id this case action is linked to, required.
        /// </summary>
        public required string CaseId { get; set; }

        /// <summary>
        /// Gets or sets the specific case this case action is linked to, required.
        /// </summary>
        public required Case Case { get; set; }
    }
}
