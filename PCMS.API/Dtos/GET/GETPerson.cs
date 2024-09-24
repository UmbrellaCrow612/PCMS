namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a Person
    /// </summary>
    public class GETPerson
    {
        public required string Id { get; set; }

        public required string FullName { get; set; }

        public required string ContactInfo { get; set; }

        public required DateTime DateOfBirth { get; set; }
    }
}