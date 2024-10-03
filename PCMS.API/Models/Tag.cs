using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class Tag : IAuditable
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation property
        public ICollection<CaseTag> CaseTags { get; set; } = [];

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedAtUtc { get; set; }

        [Required]
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; }

        public string? LastModifiedById { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }
    }

}