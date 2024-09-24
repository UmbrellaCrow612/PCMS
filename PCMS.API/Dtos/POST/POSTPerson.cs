using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO for POST a person.
    /// </summary>
    public class POSTPerson
    {

        [Required]
        public required string FullName { get; set; }

        [Required]
        public required string ContactInfo { get; set; }

        [Required]
        [NotInFuture]
        public required DateTime DateOfBirth { get; set; }
    }
}