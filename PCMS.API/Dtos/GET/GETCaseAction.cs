using PCMS.API.Models;

namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a <see cref="CaseAction"/>
    /// </summary>
    public record GETCaseAction
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Type { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public required GETApplicationUser Creator { get; set; }

        public GETApplicationUser? LastModifiedBy { get; set; }

    }
}