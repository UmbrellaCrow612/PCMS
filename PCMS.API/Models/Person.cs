namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a person in the system, outside people, not officers or users of the system.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets the Person Id. Defaults to <see cref="Guid.NewGuid()".
        /// </summary>
        public string Id { get; } = Guid.NewGuid().ToString();

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

        /// <summary>
        /// Navigation ef core
        /// </summary>
        public List<CasePerson> CasesInvolved { get; set; } = [];
    }
}
