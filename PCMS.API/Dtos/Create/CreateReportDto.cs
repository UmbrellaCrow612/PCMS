using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO for POST a report
    /// </summary>
    public class CreateReportDto
    {

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Details { get; set; }
    }
}