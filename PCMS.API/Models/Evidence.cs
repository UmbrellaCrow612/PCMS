using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents evidence linked to a case.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Evidence
    {
        /// <summary>
        /// Gets the evidence ID, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the evidence CaseId it is linked to, <see cref="Case"/>.
        /// </summary>
        [Required(ErrorMessage = "CaseId is required")]
        public required string CaseId { get; set; }

        /// <summary>
        /// EF Core navigation property, linked to <see cref="CaseId"/>.
        /// </summary>
        public Case? Case { get; set; } = null!;

        /// <summary>
        /// Gets or sets the file url of evidence.
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
        [Required(ErrorMessage = "Location is required")]
        public required string Location { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the evidence was collected.
        /// </summary>
        [Required(ErrorMessage = "CollectionDateTime is required")]
        [DataType(DataType.DateTime)]
        public required DateTime CollectionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the evidence entry.
        /// </summary>
        [Required(ErrorMessage = "CreatedById is required")]
        public required string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the evidence LastEditedBy.
        /// </summary>
        [Required(ErrorMessage = "LastEditedById is required")]
        public required string LastEditedById { get; set; }

        /// <summary>
        /// Gets or sets the person details of who collected the evidence.
        /// </summary>
        [Required(ErrorMessage = "CollectedByDetails is required")]
        public required string CollectedByDetails { get; set; }

        /// <summary>
        /// Gets or sets the evidence LastModifiedDate, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [Required(ErrorMessage = "LastModifiedDate is required")]
        public required DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the evidence CreatedAt defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [Required(ErrorMessage = "CreatedAt is required")]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}