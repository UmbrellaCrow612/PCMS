using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a case in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(CaseNumber), IsUnique = true)]
    public class Case
    {
        /// <summary>
        /// Gets or sets the Case Id. Defaults to <see cref="Guid.NewGuid()"
        /// </summary>
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the Case Number. Defaults to the call to <see cref="CaseNumberGenerator.GenerateCaseNumber()"/>
        /// </summary>
        [Required(ErrorMessage = "CaseNumber is required")]
        public string CaseNumber { get; set; } = CaseNumberGenerator.GenerateCaseNumber();

        /// <summary>
        /// Gets or sets the Case title.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the Case description.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the Case status, defaults to <see cref="CaseStatus.Open"/>.
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(CaseStatus))]
        public CaseStatus Status { get; set; } = CaseStatus.Open;

        /// <summary>
        /// Gets or sets the time the case was opened defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [Required(ErrorMessage = "DateOpened is required")]
        [DataType(DataType.DateTime)]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the time the case was closed defaults to <see cref="Nullable"/>.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateClosed { get; set; } = null;

        /// <summary>
        /// Gets or sets the date and time when the case was last modified, defaults to <see cref="DateTime.UtcNow;"/>.
        /// </summary>
        [Required(ErrorMessage = "LastModifiedDate is required")]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the case priority based on the <see cref="CasePriority"/> enum.
        /// </summary>
        [Required(ErrorMessage = "Priority is required")]
        [EnumDataType(typeof(CasePriority))]
        public required CasePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the case type.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public required string Type { get; set; }

        /// <summary>
        /// Gets or sets the user Id Who created the case.
        /// </summary>
        [Required(ErrorMessage = "CreatedById is required")]
        public required string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the user Id who last modified the case.
        /// </summary>
        [Required(ErrorMessage = "LastEditedById is required")]
        public required string LastEditedById { get; set; }

        /// <summary>
        /// Gets or sets the case actions, list of <see cref="CaseAction"/>.
        /// </summary>
        public List<CaseAction> CaseActions { get; set; } = [];

        /// <summary>
        /// Gets or sets the case reports, list of <see cref="Report"/>.
        /// </summary>
        public List<Report> Reports { get; set; } = [];

        /// <summary>
        /// Gets or sets the case evidence, list of <see cref="Evidence"/>.
        /// </summary>
        public List<Evidence> Evidences { get; set; } = [];

        /// <summary>
        /// Navigation ef core
        /// </summary>
        public List<CasePerson> PersonsInvolved { get; set; } = [];

        /// <summary>
        /// Navigation property for the users assigned to the case.
        /// A case can have many users assigned to it.
        /// </summary>
        public List<ApplicationUserCase> AssignedUsers { get; set; } = [];
    }


    /// <summary>
    /// Represents statuses a case can be in at a given time.
    /// </summary>
    public enum CaseStatus
    {
        Open = 0,
        Closed,
        InProgress,
        OnHold,
        Resolved
    }

    /// <summary>
    /// Represents priorities a case can be in at a given time.
    /// </summary>
    public enum CasePriority
    {
        Low = 0,
        Medium,
        High,
        Critical
    }

    /// <summary>
    /// PMCS implementation standard of generating unique case numbers
    /// </summary>
    public static class CaseNumberGenerator
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

        /// <summary>
        /// Method to invoke to generate a unique case number
        /// </summary>
        /// <returns>String case number</returns>
        public static string GenerateCaseNumber()
        {
            // Get the current year
            var year = DateTime.UtcNow.Year;

            // Generate a random 8-digit number
            var randomNumber = GenerateRandomNumber(8);

            // Format the case number
            var caseNumber = $"CA-{year}-{randomNumber:D8}";
            return caseNumber;
        }

        private static int GenerateRandomNumber(int digits)
        {
            byte[] randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            int randomInt = BitConverter.ToInt32(randomBytes, 0);
            return Math.Abs(randomInt) % (int)Math.Pow(10, digits);
        }
    }



}