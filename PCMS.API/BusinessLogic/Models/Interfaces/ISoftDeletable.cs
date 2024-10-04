using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models.Interfaces
{
    /// <summary>
    /// Apply to models who can only be soft deleted.
    /// </summary>
    public interface ISoftDeletable
    {
        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public string? DeletedById { get; set; }

        public ApplicationUser? UserWhoDeleted { get; set; }
    }
}
