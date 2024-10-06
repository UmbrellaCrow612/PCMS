using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO when POST a booking
    /// </summary>
    public class CreateBookingDto
    {
        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }
    }
}