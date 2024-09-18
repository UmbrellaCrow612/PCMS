using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a Incident in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Incident
    {
        /// <summary>
        /// Gets or sets the ID of the Incident, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        [Key]
        public string Id = Guid.NewGuid().ToString();

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "IncidentDateTime is required")]
        public required DateTime IncidentDateTime { get; set; }

        /// <summary>
        /// Gets or sets the IncidentType of the Incident record.
        /// </summary>
        [Required(ErrorMessage = "IncidentType is required")]
        public required string IncidentType { get; set; }

        /// <summary>
        /// Gets or sets the Description of the Incident record.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the Incident record.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; }
    }
}