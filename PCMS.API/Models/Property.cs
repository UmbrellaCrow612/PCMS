using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a property in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Property
    {
        /// <summary>
        /// Gets or sets the ID of the property, defaults to a new GUID.
        /// </summary>
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the name or title of the property.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the property.
        /// </summary>
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the property (e.g., House, Apartment, Commercial).
        /// </summary>
        [Required(ErrorMessage = "Property type is required")]
        [StringLength(50, ErrorMessage = "Property type cannot be longer than 50 characters")]
        public required string PropertyType { get; set; }

        /// <summary>
        /// Gets or sets the status of the property (e.g., For Sale, For Rent, Sold).
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters")]
        public required string Status { get; set; }

        /// <summary>
        /// Gets or sets the price of the property.
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public required decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of bedrooms in the property.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Number of bedrooms must be a non-negative number")]
        public int Bedrooms { get; set; }

        /// <summary>
        /// Gets or sets the number of bathrooms in the property.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Number of bathrooms must be a non-negative number")]
        public decimal Bathrooms { get; set; }

        /// <summary>
        /// Gets or sets the total area of the property in square feet/meters.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Area must be a positive number")]
        public decimal Area { get; set; }

        /// <summary>
        /// Gets or sets the year the property was built.
        /// </summary>
        [Range(1600, 9999, ErrorMessage = "Year built must be between 1600 and 9999")]
        public int? YearBuilt { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the property record.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the last update date of the property record.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated location.
        /// </summary>
        [Required(ErrorMessage = "Location ID is required")]
        public required string LocationId { get; set; }

        /// <summary>
        /// EF Core nav.
        /// </summary>
        public Location? Location { get; set; } = null!;
    }
}