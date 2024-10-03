using PCMS.API.DTOS.GET;
using PCMS.API.Models;
using PCMS.API.Models.Enums;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you want to get a <see cref="CaseEdit"/>
    /// </summary>
    public class CaseEditDto
    {
        public required string Id { get; set; }

        public required ApplicationUserDto Creator { get; set; }

        public required DateTime CreatedAtUtc { get; set; }

        public required string PreviousTitle { get; set; }

        public required string PreviousDescription { get; set; }

        public required CaseComplexity PreviousComplexity { get; set; }

        public required CaseStatus PreviousStatus { get; set; }

        public required CasePriority PreviousPriority { get; set; }

        public required string PreviousType { get; set; }
    }
}