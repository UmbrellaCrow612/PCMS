namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for GET a user object
    /// </summary>
    public class GETApplicationUser
    {
        /// <summary>
        /// Get or set user Id
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user FirstName
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user LastName
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user Rank
        /// </summary>
        public string Rank { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user BadgeNumber
        /// </summary>
        public string BadgeNumber { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user DOB
        /// </summary>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Get or set user UserName
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Get or set user Email
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
