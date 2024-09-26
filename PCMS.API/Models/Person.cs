using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a person in the system, outside people, not officers or users of the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(FullName))]
    public class Person
    {

        [Key]
        public string Id { get; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        [Required]
        public required string ContactInfo { get; set; }

        [Required]
        public required DateTime DateOfBirth { get; set; }



        public ICollection<CasePerson> CasesInvolved { get; set; } = [];

        public ICollection<Booking> Bookings { get; set; } = [];

        public ICollection<Release> Releases { get; set; } = [];

        public ICollection<Charge> Charges { get; set; } = [];
    }
}