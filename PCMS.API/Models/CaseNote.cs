using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class CaseNote : ISoftDeletable
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public DateTime? LastModifiedDate { get; set; }



        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null!;

        [Required]
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; } = null;

        public string? LastEditedById { get; set; }

        public ApplicationUser? LastEditor { get; set; } = null;

        public string? DeletedById { get; set; }

        public ApplicationUser? UserWhoDeleted { get; set; } = null;
    }
}