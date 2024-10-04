using PCMS.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO when you want to create a charge for a booking
    /// </summary>
    public class CreateChargeDto
    {
        [Required]
        [StringLength(300)]
        public required string Offense { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [NotInFuture]
        public required DateTime DateCharged { get; set; }

        [Required]
        [StringLength(300)]
        public required string Description { get; set; }
    }
}