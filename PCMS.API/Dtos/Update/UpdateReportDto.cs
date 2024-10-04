using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.Update
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