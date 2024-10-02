using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class CaseNote : ISoftDeletable, IAuditable
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedAtUtc { get; set; }



        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null!;

        [Required]
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; } = null;

        public string? DeletedById { get; set; }

        public ApplicationUser? UserWhoDeleted { get; set; } = null;

        public string? LastModifiedById { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }
    }
}