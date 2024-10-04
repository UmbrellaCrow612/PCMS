using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.Update
{
    /// <summary>
    /// DTO for when you want to update a Person
    /// </summary
    public class UpdatePersonDto
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