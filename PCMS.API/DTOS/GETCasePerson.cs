using PCMS.API.Models;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for GET Case Person
    /// </summary>
    public class GETCasePerson
    {
        public required string Id { get; set; }

        public required string CaseId { get; set; }

        public required string PersonId { get; set; }

        public required CaseRole Role { get; set; }
    }
}
