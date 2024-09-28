using Microsoft.EntityFrameworkCore;
using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// A Release in the system
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Release
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [DataType(DataType.DateTime)]
        [NotInFuture]
        public required DateTime ReleaseDate { get; set; }

        [Required]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; } = null!;

        [Required]
        public required string BookingId { get; set; }

        public Booking? Booking { get; set; } = null!;


        [Required]
        public required string PersonId { get; set; }

        public Person? Person { get; set; } = null!;


        [Required]
        [StringLength(100)]
        public required string ReleaseType { get; set; }

        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }
    }
}