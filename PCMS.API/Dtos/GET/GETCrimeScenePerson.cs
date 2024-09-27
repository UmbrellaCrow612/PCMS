using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you get a crime scene person
    /// </summary>
    public class GETCrimeScenePerson
    {
        public required string Id { get; set; }

        public required CrimeSceneRole Role { get; set; }

        public required GETPerson Person { get; set; }
    }
}
