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
        [Key]
        public string Id { get; } = Guid.NewGuid().ToString();

        [Required]
        public required string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }

        [Required]
        public required string Details { get; set; }



        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null;

        [Required]
        public required string CreatedById { get; set; }

        public ApplicationUser? Creator { get; set; } = null;

        public string? LastEditedById { get; set; }

        public ApplicationUser? LastEditor { get; set; } = null;
    }
}