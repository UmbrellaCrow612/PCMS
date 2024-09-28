using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace PCMS.API.Models
{
    /// <summary>
    /// Many to many relation between person and a crime scene
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CrimeScenePerson
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public required CrimeSceneRole Role { get; set; }

        [Required]
        public required string CrimeSceneId { get; set; }

        public CrimeScene? CrimeScene { get; set; } = null;

        [Required]
        public required string PersonId { get; set; }

        public Person? Person { get; set; } = null;


    }
}
