namespace PCMS.API.Dtos.Read
{
    public class VehicleDto
    {
        public required string Id { get; set; }

        public required string Make { get; set; }

        public required string Model { get; set; }

        public required int Year { get; set; }

        public required string VIN { get; set; }

        public required string LicensePlate { get; set; }

        public string? Description { get; set; }

        public required string Color { get; set; }
    }
}
