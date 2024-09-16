namespace PCMS.API.Models
{
    /// <summary>
    /// Join table between <see cref="Case"/> and <see cref="Person"/> i.e a person is linked to a case as a witness, suspect
    /// or victim
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
