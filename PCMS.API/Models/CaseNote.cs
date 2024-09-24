using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class CaseNote
    {
        /// <summary>
        /// Gets or sets the CaseNote Id. Defaults to <see cref="Guid.NewGuid()"/>
        /// </summary>
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the CaseNote CaseId.
        /// </summary>
        [Required(ErrorMessage = "CaseId is required")]
        public required string CaseId { get; set; }

        /// <summary>
        /// EF Core navigation property.
        /// </summary>
        public Case? Case { get; set; } = null!;

        /// <summary>
        /// Gets or sets the CaseNote Description.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the CaseNote CreatedAt.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the CaseNote CreatedById of the user.
        /// </summary>
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; } = null;

        /// <summary>
        /// Gets or sets the last update time of the CaseNote.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }
    }
}