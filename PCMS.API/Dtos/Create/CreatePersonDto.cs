using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO for POST a person.
    /// </summary>
    public class CreatePersonDto
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