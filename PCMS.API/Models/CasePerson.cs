namespace PCMS.API.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between case and person.
    /// </summary>
    public class CasePerson
    {
        public required string CaseId { get; set; }
        public Case Case { get; set; } = null!;

        public required string PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public required CaseRole Role { get; set; }
    }
}
