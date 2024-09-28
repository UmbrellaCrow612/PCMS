using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between case and person.
    /// </summary>
    /// <remarks>
    /// A person can have multiple links or CasePerson for or on multiple cases, for example they could be both the 
    /// victim and witness so you could link them as both.
    /// </remarks>
    [Index(nameof(Id), IsUnique = true)]
    public class CasePerson
    {
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "CaseId is required")]
        public required string CaseId { get; set; }
        public Case? Case { get; set; } = null;

        [Required(ErrorMessage = "PersonId is required")]
        public required string PersonId { get; set; }
        public Person? Person { get; set; } = null;

        [Required(ErrorMessage = "Role is required")]
        [EnumDataType(typeof(CaseRole))]
        public required CaseRole Role { get; set; }
    }
}