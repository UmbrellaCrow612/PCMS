using PCMS.API.Filters;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO when you want to create a <see cref="Case"/>
    /// </summary>
    public record POSTCase
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; } 

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