namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO to get a case note.
    /// </summary>
    public class GETCaseNote
    {
        public required string Id { get; set; }

        public required string CaseId { get; set; }

        public required string Description { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public required string CreatedById { get; set; }

        public string? UpdatedById { get; set; }
    }
}
