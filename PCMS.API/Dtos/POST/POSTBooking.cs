using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.POST
{
    /// <summary>
    /// DTO when POST a booking
    /// </summary>
    public class POSTBooking
    {
        [Required]
        public required string LocationId { get; set; }

        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }
    }
}
