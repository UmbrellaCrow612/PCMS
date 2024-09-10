using System.Security.Cryptography;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a case in the system.
    /// </summary>
    public class Case
    {
        /// <summary>
        /// Gets or sets the Case Id.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the Case Number.
        /// </summary>
        public string CaseNumber { get; set; } = string.Empty;

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
        /// Method to invoke to generate a unique case numbers
        /// <returns>String case number</returns>
        /// </summary>
        public static string GenerateCaseNumber()
        {
            // Get the current timestamp
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            // Generate a random alphanumeric string
            var randomString = GenerateRandomString(4);

            // Use a single prefix "PM"
            var prefix = "PM";

            // Format the case number
            var caseNumber = $"{prefix}-{timestamp}-{randomString}";

            return caseNumber;
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomChars = new char[length];
            var randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            for (int i = 0; i < length; i++)
            {
                randomChars[i] = chars[randomBytes[i] % chars.Length];
            }
            return new string(randomChars);
        }
    }



}
