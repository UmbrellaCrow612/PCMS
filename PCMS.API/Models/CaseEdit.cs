using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a Edit on a case in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CaseEdit
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string UserId { get; set; }

        public ApplicationUser? User { get; set; } = null!;

        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null!;

        [Required]
        public required string PreviousTitle { get; set; }

        [Required]
        public required string PreviousDescription { get; set; }

        [Required]
        public required CaseStatus PreviousStatus { get; set; }

        [Required]
        public required CasePriority PreviousPriority { get; set; }

        [Required]
        public required string PreviousType { get; set; }

        public required CaseComplexity PreviousComplexity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
