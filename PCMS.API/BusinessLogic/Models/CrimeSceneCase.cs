using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Many to many between linking a crime scenes and cases
    /// </summary>
    public class CrimeSceneCase
    {
        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null;

        [Required]
        public required string CrimeSceneId { get; set; }

        public CrimeScene? CrimeScene { get; set; } = null;
    }
}