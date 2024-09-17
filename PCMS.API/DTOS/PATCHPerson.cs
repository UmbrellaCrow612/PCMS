using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for PATCH a person.
    /// </summary>
    public class PATCHPerson
    {
        /// <summary>
        /// Gets or sets the Person FullName.
        /// </summary>
        [Required(ErrorMessage = "FullName is required")]
        public required string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Person ContactInfo.
        /// </summary>
        [Required(ErrorMessage = "ContactInfo is required")]
        public required string ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the Person DateOfBirth.
        /// </summary>
        [Required(ErrorMessage = "DateOfBirth is required")]
        [NotInFuture(ErrorMessage = "DateOfBirth can not be in the future")]
        public required DateTime DateOfBirth { get; set; }
    }
}
