using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO for POST a report
    /// </summary>
    public class POSTReport
    {
        /// <summary>
        /// Get or set the report title.
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