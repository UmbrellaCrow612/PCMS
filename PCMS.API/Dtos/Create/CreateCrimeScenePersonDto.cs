using PCMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO wen you want to link a person to a crime scene
    /// </summary>
    public class CreateCrimeScenePersonDto
    {
        [Required]
        [EnumDataType(typeof(CrimeSceneRole))]
        public required CrimeSceneRole Role { get; set; }
    }
}