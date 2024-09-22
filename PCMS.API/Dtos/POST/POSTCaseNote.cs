using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.POST
{
    /// <summary>
    /// DTO to POST a Case Note.
    /// </summary>
    public class POSTCaseNote
    {
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }
    }
}