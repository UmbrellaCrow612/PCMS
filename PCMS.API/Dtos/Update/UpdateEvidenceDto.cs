using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO for when you want to update a Evidence
    /// </summary
    public class UpdateEvidenceDto
    {
        [Required]
        public required string FileUrl { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        public required string Description { get; set; }

        public required string Location { get; set; }

        [NotInFuture]
        public required DateTime CollectionDateTime { get; set; }

        public required string CollectedByDetails { get; set; }
    }
}