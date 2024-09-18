using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO for a PATCH report.
    /// </summary>
    public class PATCHReport
    {
        /// <summary>
        /// Gets or sets the report Title.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the report Details.
        /// </summary>
        [Required(ErrorMessage = "Details is required")]
        public string Details { get; set; } = string.Empty;
    }
}