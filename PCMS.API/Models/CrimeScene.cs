using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a CrimeScene in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CrimeScene
    {
        /// <summary>
        /// Gets or sets the ID of the CrimeScene, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        [Key]
        public string Id = Guid.NewGuid().ToString();
    }
}