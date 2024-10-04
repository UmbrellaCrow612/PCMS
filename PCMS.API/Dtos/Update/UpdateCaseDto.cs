using PCMS.API.BusinessLogic.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.Update
{
    /// <summary>
    /// DTO for when you want to update a Case
    /// </summary
    public record UpdateCaseDto
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [EnumDataType(typeof(CaseStatus))]
        public CaseStatus Status { get; set; }

        [Required]
        [EnumDataType(typeof(CasePriority))]
        public CasePriority Priority { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        [EnumDataType(typeof(CaseComplexity))]
        public required CaseComplexity Complexity { get; set; }
    }
}