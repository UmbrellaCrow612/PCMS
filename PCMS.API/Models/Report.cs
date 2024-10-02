using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a report in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Report : ISoftDeletable, IAuditable
    {
        [Key]
        public string Id { get; } = Guid.NewGuid().ToString();

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Details { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAtUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedAtUtc { get; set; }




        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null;

        public string? DeletedById { get; set; }

        public ApplicationUser? UserWhoDeleted { get; set; }

        [Required]
        public required string CreatedById { get; set; }
        public ApplicationUser? Creator { get; set; }

        public string? LastModifiedById { get; set; }
        public ApplicationUser? LastModifiedBy { get; set; }
    }
}