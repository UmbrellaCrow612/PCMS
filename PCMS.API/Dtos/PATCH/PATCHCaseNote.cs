using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.PATCH
{
    /// <summary>
    /// DTO to PATCH a Case Note
    /// </summary>
    public class PATCHCaseNote
    {
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }
    }
}
