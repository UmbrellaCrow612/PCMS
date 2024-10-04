using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.PATCH
{
    /// <summary>
    /// DTO for when you want to update a case note
    /// </summary
    public class UpdateCaseNoteDto
    {
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }
    }
}