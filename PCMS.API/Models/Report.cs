using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a report in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Report
    {
        /// <summary>
        /// Gets the report ID, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the report Title.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the report CreatedById.
        /// </summary>
        [Required(ErrorMessage = "CreatedById is required")]
        public required string CreatedById { get; set; }

        /// <summary>
        /// EF Core <see cref="CreatedById"/>
        /// </summary>
        public required ApplicationUser Creator { get; set; }

        /// <summary>
        /// Gets or sets the report LastEditedBy.
        /// </summary>
        public string? LastEditedById { get; set; }

        /// <summary>
        /// EF Core <see cref="LastEditedById"/>
        /// </summary>
        public ApplicationUser? LastEditor { get; set; }

        /// <summary>
        /// Gets or sets the report CreatedAt defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [Required(ErrorMessage = "CreatedAt is required")]
        [DataType(DataType.DateTime)]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the report LastModifiedDate, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the report Details.
        /// </summary>
        [Required(ErrorMessage = "Details is required")]
        public required string Details { get; set; }

        /// <summary>
        /// Gets or sets the report CaseId it is linked to <see cref="Case"/>.
        /// </summary>
        [Required(ErrorMessage = "CaseId is required")]
        public required string CaseId { get; set; }

        /// <summary>
        /// Reference navigation property for <see cref="CaseId"/>.
        /// </summary>
        public Case? Case { get; set; } = null!;
    }
}