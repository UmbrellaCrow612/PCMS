using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.PATCH
{
    /// <summary>
    /// DTO for when you want to update a Booking
    /// </summary
    public class UpdateBookingDto
    {
        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }
    }
}