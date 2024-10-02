namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a Evidence
    /// </summary>
    public class GETEvidence
    {
        public required string Id { get; set; }

        public required string FileUrl { get; set; }

        public required string Type { get; set; }

        public required string Description { get; set; }

        public required string Location { get; set; }

        public required DateTime CollectionDateTime { get; set; }

        public required DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public required GETApplicationUser Creator { get; set; }

        public required GETApplicationUser LastModifiedBy { get; set; }

        public required string CollectedByDetails { get; set; }

    }
}