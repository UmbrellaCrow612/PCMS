namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO for GET a person.
    /// </summary>
    public class GETPerson
    {
        /// <summary>
        /// Gets the Person Id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the Person FullName.
        /// </summary>
        public required string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Person ContactInfo.
        /// </summary>
        public required string ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the Person DateOfBirth.
        /// </summary>
        public required DateTime DateOfBirth { get; set; }
    }
}
