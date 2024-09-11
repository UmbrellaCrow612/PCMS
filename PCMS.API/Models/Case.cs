using System.Security.Cryptography;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a case in the system.
    /// </summary>
    public class Case
    {
        /// <summary>
        /// Gets or sets the Case Id. Defaults to <see cref="Guid.NewGuid()"
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the Case Number. Defaults to the call to <see cref="CaseNumberGenerator.GenerateCaseNumber()"/>
        /// </summary>
        public string CaseNumber { get; set; } = CaseNumberGenerator.GenerateCaseNumber();

        /// <summary>
        /// Gets or sets the Case title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case status based on the <see cref="CaseStatus"/> enum.
        /// </summary>
        public CaseStatus Status { get; set; } = CaseStatus.Open;

        /// <summary>
        /// Gets or sets the time the case was opened defaults to now.
        /// </summary>
        public DateTime DateOpened { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the time the case was closed defaults to null.
        /// </summary>
        public DateTime? DateClosed { get; set; } = null;

        /// <summary>
        /// Gets or sets the date and time when the case was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the case priority based on the <see cref="CasePriority"/> enum.
        /// </summary>
        public CasePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the case type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user Id Who created the case.
        /// </summary>
        public required string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the user Id who last modified the case.
        /// </summary>
        public required string LastModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the case actions, list of CaseAction.
        /// </summary>
        public List<CaseAction> CaseActions { get; set; } = [];

        /// <summary>
        /// Gets or sets the case assigned users, list of ApplicationUser.
        /// </summary>
        public List<ApplicationUser> AssignedUsers { get; set; } = [];

        /// <summary>
        /// Gets or sets the case reports, list of Report.
        /// </summary>
        public List<Report> Reports { get; set; } = [];
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
