using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Models.Enums;
using PCMS.API.BusinessLogic.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Represents a Edit on a case in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CaseEdit : IAuditCreator
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; }

        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null!;

        [Required]
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

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

    }
}