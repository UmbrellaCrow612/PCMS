using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a specific action taken on a case.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CaseAction
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }



        [Required]
        public required string CreatedById { get; set; }

        public required ApplicationUser Creator { get; set; }

        public string? LastEditedById { get; set; }

        public ApplicationUser? LastEditor { get; set; }

        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null;
    }
}