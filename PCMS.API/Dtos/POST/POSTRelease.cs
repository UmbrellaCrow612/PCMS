using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.POST
{
    /// <summary>
    /// DTO when you want to make a release for a booking
    /// </summary>
    public class POSTRelease
    {
        [Required]
        [StringLength(100)]
        public required string ReleaseType { get; set; }

        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }
    }
}