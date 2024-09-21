using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO for a PATCH a case
    /// </summary>
    public record PATCHCase
    {
        /// <summary>
        /// Gets or sets the Case Title.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case Description.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case Status.
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(CaseStatus))]
        public CaseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the Case Priority.
        /// </summary>
        [Required(ErrorMessage = "Priority is required")]
        [EnumDataType(typeof(CasePriority))]
        public CasePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the Case Type.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(CaseComplexity))]
        public required CaseComplexity Complexity { get; set; }
    }
}