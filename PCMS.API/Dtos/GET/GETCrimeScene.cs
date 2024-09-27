using PCMS.API.DTOS.GET;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// Dto when you want to get a crime scene
    /// </summary>
    public class GETCrimeScene
    {
        public required string Id { get; set; }

        public required string Type { get; set; }

        public required string Description { get; set; }

        public required DateTime ReportedDateTime { get; set; }

        public required DateTime DiscoveredDateTime { get; set; }

        public required  GETLocation Location { get; set; }
    }
}
