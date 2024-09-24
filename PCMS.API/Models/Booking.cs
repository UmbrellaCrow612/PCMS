﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// A booking in the system
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Booking
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Required]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; } = null!;

        [Required]
        public required string PersonId { get; set; }

        public Person? Person { get; set; } = null!;


        public List<Charge> Charges { get; set; } = [];


        public string? ReleaseId { get; set; }

        public Release? Release { get; set; } = null!;


        [Required]
        public required string LocationId { get; set; }

        public Location? Location { get; set; } = null!;

        [Required]
        [StringLength(300)]
        public required string Notes { get; set; }
    }
}
