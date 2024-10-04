namespace PCMS.API.DTOS.Read
{
    /// <summary>
    /// DTO when you want to get a Property
    /// </summary>
    public class PropertyDto
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public required string PropertyType { get; set; }

        public required string Status { get; set; }

        public required decimal Price { get; set; }

        public int Bedrooms { get; set; }

        public decimal Bathrooms { get; set; }

        public decimal Area { get; set; }

        public int? YearBuilt { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public required LocationDto Location { get; set; }
    }
}