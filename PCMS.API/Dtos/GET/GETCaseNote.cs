using PCMS.API.DTOS.GET;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you want to get a CaseNote
    /// </summary>
    public class GETCaseNote
    {
        public required string Id { get; set; }

        public required string Description { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public required GETApplicationUser Creator { get; set; }

    }
}