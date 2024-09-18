using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO for a PATCH a evidence item.
    /// </summary>
    public class PATCHEvidence
    {
        /// <summary>
        /// Gets or sets the Evidence FileUrl.
        /// </summary>
        [Required(ErrorMessage = "FileUrl is required")]
        public required string FileUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of evidence (e.g., Physical, Digital, Documentary).
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public required string Type { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the evidence.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the location where the evidence is stored.
        /// </summary>
        public required string Location { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the evidence was collected.
        /// </summary>
        public required DateTime CollectionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the person details of who collected the evidence.
        /// </summary>
        public required string CollectedByDetails { get; set; }
    }
}
