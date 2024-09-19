using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO to PATCH a Property.
    /// </summary>
    public class PATCHProperty
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Property type is required")]
        [StringLength(50, ErrorMessage = "Property type cannot be longer than 50 characters")]
        public required string PropertyType { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters")]
        public required string Status { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public required decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of bedrooms must be a non-negative number")]
        public int Bedrooms { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Number of bathrooms must be a non-negative number")]
        public decimal Bathrooms { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Area must be a positive number")]
        public decimal Area { get; set; }

        [Range(1600, 9999, ErrorMessage = "Year built must be between 1600 and 9999")]
        public int? YearBuilt { get; set; }

        [Required(ErrorMessage = "Location ID is required")]
        public required string LocationId { get; set; }
    }
}
