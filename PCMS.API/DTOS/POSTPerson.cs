using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for POST a person.
    /// </summary>
    public class POSTPerson
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
        public required DateTime DateOfBirth { get; set; }
    }
}
