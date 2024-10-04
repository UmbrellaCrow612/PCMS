using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// A Vehicle in the system, representing a car, motorcycle, or other type of vehicle.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Vehicle
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public required string Make { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(17)]
        public required string VIN { get; set; }

        [Required]
        [MaxLength(20)]
        public required string LicensePlate { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Color { get; set; }

        public ICollection<CaseVehicle> CaseVehicles { get; set; } = [];
    }
}
