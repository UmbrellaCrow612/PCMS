using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.PATCH
{
    /// <summary>
    /// DTO when you want to update a property
    /// </summary>
    public class PATCHProperty
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public required string PropertyType { get; set; }

        [Required]
        [StringLength(20)]
        public required string Status { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public required decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Bedrooms { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Bathrooms { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Area { get; set; }

        [Range(1600, 9999)]
        public int? YearBuilt { get; set; }

        [Required]
        public required string LocationId { get; set; }
    }
}