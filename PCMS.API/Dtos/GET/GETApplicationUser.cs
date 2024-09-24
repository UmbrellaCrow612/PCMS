namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a user
    /// </summary>
    public record GETApplicationUser
    {
        public required string Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Rank { get; set; }

        public required string BadgeNumber { get; set; }

        public required DateTime DOB { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }
    }
}