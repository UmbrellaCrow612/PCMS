namespace PCMS.API.DTOS
{
    public class GETApplicationUser
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Rank { get; set; } = string.Empty;

        public string BadgeNumber { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
