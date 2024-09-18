using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a location in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Location
    {
        /// <summary>
        /// Gets or sets the ID of the location, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public required string Address { get; set; }

        /// <summary>
        /// Gets or sets the city of the location.
        /// </summary>
        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public required string City { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the location.
        /// </summary>
        [Required(ErrorMessage = "PostalCode is required")]
        [StringLength(20, ErrorMessage = "PostalCode cannot be longer than 20 characters")]
        public required string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the location.
        /// </summary>
        [Required(ErrorMessage = "Latitude is required")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public required decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the location.
        /// </summary>
        [Required(ErrorMessage = "Longitude is required")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public required decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the location record.
        /// </summary>
        [Required(ErrorMessage = "CreatedAt is required")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the last update date of the location record.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// EF Core nav
        /// </summary>
        public ICollection<Property> Properties { get; set; } = [];
    }
}