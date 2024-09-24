using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO for POST a report
    /// </summary>
    public class POSTReport
    {

        [Required]
        public required string Title { get; set; } 

        [Required]
        public required string Details { get; set; } 
    }
}