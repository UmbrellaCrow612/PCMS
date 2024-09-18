namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO to GET a location.
    /// </summary>
    public class GETLocation
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Address { get; set; }

        public required string City { get; set; }

        public required string PostalCode { get; set; }

        public required decimal Latitude { get; set; }

        public required decimal Longitude { get; set; }
    }
}
