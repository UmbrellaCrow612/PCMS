using PCMS.API.Dtos.GET;
using PCMS.API.Models;
using PCMS.API.Models.Enums;

namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a <see cref="Case"/>
    /// </summary>
    public record GETCase
    {
        public required string Id { get; set; }

        public required string CaseNumber { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required CaseStatus Status { get; set; }

        public required DateTime DateOpened { get; set; }

        public required DateTime? DateClosed { get; set; }

        public required DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public required CasePriority Priority { get; set; }

        public required string Type { get; set; } = string.Empty;

        public required GETApplicationUser Creator { get; set; }

        public GETApplicationUser? LastModifiedBy { get; set; }

        public required CaseComplexity Complexity { get; set; }

        public required GETDepartment Department { get; set; }
    }
}