namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for GET a user object
    /// </summary>
    public record GETApplicationUser
    {
        /// <summary>
        /// Get or set user Id
        /// </summary>
        public required string Id { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user FirstName
        /// </summary>
        public required string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user LastName
        /// </summary>
        public required string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user Rank
        /// </summary>
        public required string Rank { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user BadgeNumber
        /// </summary>
        public required string BadgeNumber { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user DOB
        /// </summary>
        public required DateTime DOB { get; set; }

        /// <summary>
        /// Get or set user UserName
        /// </summary>
        public required string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user Email
        /// </summary>
        public required string Email { get; set; } = string.Empty;
    }
}