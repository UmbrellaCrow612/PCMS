using PCMS.API.DTOS.Read;

namespace PCMS.API.Dtos.Read
{
    /// <summary>
    /// Dto when you want to get a crime scene
    /// </summary>
    public class CrimeSceneDto
    {
        public required string Id { get; set; }

        public required string Type { get; set; }

        public required string Description { get; set; }

        public required DateTime ReportedDateTime { get; set; }

        public required DateTime DiscoveredDateTime { get; set; }

        public required LocationDto Location { get; set; }
    }
}