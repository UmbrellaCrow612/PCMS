using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO to POST a Case Note.
    /// </summary>
    public class CreateCaseNoteDto
    {
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }
    }
}