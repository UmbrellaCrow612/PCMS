using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for the POST a case request.
    /// </summary>
    public record POSTCase
    {
        /// <summary>
        /// Get or set the case title..
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Get or set the case description..
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Get or set the case priority based on the enum <see cref="CasePriority"/>.
        /// </summary>
        [Required(ErrorMessage = "Priority is required")]
        public CasePriority Priority { get; set; }

        /// <summary>
        /// Get or set the case type />.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

    }
}
