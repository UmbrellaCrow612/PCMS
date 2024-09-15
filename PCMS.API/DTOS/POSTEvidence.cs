using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO to POST a Evidence.
    /// </summary>
    public class POSTEvidence
    {
        /// <summary>
        /// Gets or sets the file url of evidence.
        /// </summary>
        [Required(ErrorMessage = "FileUrl is required")]
        public string FileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of evidence (e.g., Physical, Digital, Documentary).
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the evidence.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location where the evidence is stored.
        /// </summary>
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the evidence was collected.
        /// </summary>
        [Required(ErrorMessage = "CollectionDateTime is required")]
        public DateTime CollectionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the person details of who collected the evidence.
        /// </summary>
        [Required(ErrorMessage = "CollectedByDetails is required")]
        public required string CollectedByDetails { get; set; } = string.Empty;
    }
}