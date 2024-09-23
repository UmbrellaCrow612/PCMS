using Microsoft.EntityFrameworkCore;
using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// A Charge in the system
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Charge
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(300)]
        public required string Offense { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [NotInFuture]
        public required DateTime DateCharged { get; set; }

        [Required]
        [StringLength(300)]
        public required string Description { get; set; }

        [Required]
        public required string BookingId { get; set; }

        public Booking? Booking { get; set; } = null!;

        [Required]
        public required string UserId { get; set; }

        public ApplicationUser? User { get; set; } = null!;
    }
}
