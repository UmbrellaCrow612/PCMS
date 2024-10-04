using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Represents a specific action taken on a case.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CaseAction : IAuditable
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Type { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        [Required]
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; }

        public string? LastModifiedById { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }

        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null;


    }
}