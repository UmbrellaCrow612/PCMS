using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO when you want to create a crime scene
    /// </summary>
    public class CreateCrimeSceneDto
    {
        [Required]
        public required string Type { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required DateTime ReportedDateTime { get; set; }

        [Required]
        public required DateTime DiscoveredDateTime { get; set; }
    }
}