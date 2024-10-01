using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models.Interfaces
{
    /// <summary>
    /// Audit fields we want on models.
    /// </summary>
    public interface IAuditable
    {
        [Required]
        DateTime CreatedAtUtc { get; set; }

        [Required]
        string CreatedById { get; set; }

        ApplicationUser? Creator { get; set; }

        DateTime? LastModifiedAtUtc { get; set; }

        string? LastModifiedById { get; set; }

        ApplicationUser? LastModifiedBy { get; set; }
    }
}
