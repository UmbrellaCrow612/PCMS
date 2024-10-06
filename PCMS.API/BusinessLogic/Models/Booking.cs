using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// A booking in the system
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Booking : IAuditable
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedAtUtc { get; set; }

        [Required]
        public required string CreatedById { get; set; }
        public ApplicationUser? Creator { get; set; }

        [Required]
        public required string PersonId { get; set; }

        public Person? Person { get; set; } = null!;


        public List<Charge> Charges { get; set; } = [];


        public string? ReleaseId { get; set; }

        public Release? Release { get; set; } = null!;

        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }

        public string? LastModifiedById { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }
    }
}