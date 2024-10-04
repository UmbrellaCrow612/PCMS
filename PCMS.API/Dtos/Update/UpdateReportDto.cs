using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO when you want to update a report
    /// </summary>
    public class UpdateReportDto
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Details { get; set; }
    }
}