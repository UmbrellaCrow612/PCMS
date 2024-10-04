namespace PCMS.API.DTOS.Read
{
    /// <summary>
    /// DTO when you want to get a user
    /// </summary>
    public record ApplicationUserDto
    {
        public required string Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Rank { get; set; }

        public string? BadgeNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }
    }
}