using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Crime scene in the system
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CrimeScene
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required string Type { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required DateTime ReportedDateTime { get; set; }

        [Required]
        public required DateTime DiscoveredDateTime { get; set; }

        [Required]
        public required string LocationId { get; set; }

        public Location? Location { get; set; } = null;

        public ICollection<CrimeSceneCase> CrimeSceneCases { get; set; } = [];

        public ICollection<CrimeScenePerson> CrimeScenePersons { get; set; } = [];
    }
}