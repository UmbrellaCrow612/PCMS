using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO to POST a case action
    /// </summary>
    public record CreateCaseActionDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Type { get; set; }
    }
}