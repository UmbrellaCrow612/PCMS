using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.PATCH
{
    /// <summary>
    /// DTO when you want to update a tag
    /// </summary>
    public class PATCHTag
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}