using PCMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.POST
{
    /// <summary>
    /// DTO wen you want to link a person to a crime scene
    /// </summary>
    public class POSTCrimeScenePerson
    {
        [Required]
        [EnumDataType(typeof(CrimeSceneRole))]
        public required CrimeSceneRole Role { get; set; }
    }
}
