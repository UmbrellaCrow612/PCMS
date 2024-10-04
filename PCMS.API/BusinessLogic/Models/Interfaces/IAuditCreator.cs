using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models.Interfaces
{
    /// <summary>
    /// When you want to audit a model for its creator
    /// </summary>
    public interface IAuditCreator
    {
        [Required]
        DateTime CreatedAtUtc { get; set; }

        [Required]
        string CreatedById { get; set; }

        ApplicationUser? Creator { get; set; }
    }
}
