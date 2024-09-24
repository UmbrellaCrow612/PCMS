using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a person in the system, outside people, not officers or users of the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Person
    {
        /// <summary>
        /// Gets the Person Id. Defaults to <see cref="Guid.NewGuid()".
        /// </summary>
        [Key]
        public string Id { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the Person FullName.
        /// </summary>
        [Required(ErrorMessage = "FullName is required")]
        public required string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Person ContactInfo.
        /// </summary>
        [Required(ErrorMessage = "ContactInfo is required")]
        public required string ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the Person DateOfBirth.
        /// </summary>
        [Required(ErrorMessage = "DateOfBirth is required")]
        public required DateTime DateOfBirth { get; set; }


        public ICollection<CasePerson> CasesInvolved { get; set; } = [];

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}