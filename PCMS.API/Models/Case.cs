using Microsoft.EntityFrameworkCore;
using PCMS.API.Models.Enums;
using PCMS.API.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a case in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(CaseNumber), IsUnique = true)]
    public class Case : ISoftDeletable, IAuditable
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string CaseNumber { get; set; } = CaseNumberGenerator.GenerateCaseNumber();

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [EnumDataType(typeof(CaseStatus))]
        public CaseStatus Status { get; set; } = CaseStatus.Open;

        [Required]
        [EnumDataType(typeof(CaseComplexity))]
        public required CaseComplexity Complexity { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? DateClosed { get; set; } = null;

        [Required]
        [EnumDataType(typeof(CasePriority))]
        public required CasePriority Priority { get; set; }

        [Required]
        public required string Type { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTime? DeletedAtUtc { get; set; }

        public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedAtUtc { get; set; }

        [Required]
        public required string CreatedById { get; set; }
        public ApplicationUser? Creator { get; set; } = null;

        public string? LastModifiedById { get; set; }
        public ApplicationUser? LastModifiedBy { get; set; }

        public string? DeletedById { get; set; }
        public ApplicationUser? UserWhoDeleted { get; set; }

        public string? DepartmentId { get; set; }

        public Department? Department { get; set; } = null!;

        public List<CaseAction> CaseActions { get; set; } = [];

        public List<Report> Reports { get; set; } = [];

        public List<Evidence> Evidences { get; set; } = [];

        public ICollection<CaseNote> CaseNotes { get; set; } = [];

        public ICollection<CaseEdit> CaseEdits { get; set; } = [];

        public ICollection<CaseTag> CaseTags { get; set; } = [];


        public ICollection<CrimeSceneCase> CrimeSceneCases = [];

        public ICollection<CaseVehicle> CaseVehicles { get; set; } = [];

        public List<CasePerson> PersonsInvolved { get; set; } = [];

        public List<ApplicationUserCase> AssignedUsers { get; set; } = [];
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