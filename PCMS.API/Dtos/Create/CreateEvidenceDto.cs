using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO to POST a Evidence.
    /// </summary>
    public class CreateEvidenceDto
    {
        [Required]
        public required string FileUrl { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required]
        public DateTime CollectionDateTime { get; set; }

        [Required]
        public required string CollectedByDetails { get; set; }
    }
}