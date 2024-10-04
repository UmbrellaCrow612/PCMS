using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Many to many relation between a <see cref="Vehicle"/> and <see cref="Case"/>
    /// </summary>
    public class CaseVehicle
    {
        [Required]
        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null;


        [Required]
        public required string VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; } = null;
    }
}
