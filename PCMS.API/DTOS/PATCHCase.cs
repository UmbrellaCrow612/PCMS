using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for a PATCH a case
    /// </summary>
    public class PATCHCase
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
        public CaseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the Case Priority.
        /// </summary>
        [Required(ErrorMessage = "Priority is required")]
        public CasePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the Case Type.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case LastModifiedById.
        /// </summary>
        [Required(ErrorMessage = "LastModifiedById is required")]
        public string LastModifiedById { get; set; } = string.Empty;
    }
}
