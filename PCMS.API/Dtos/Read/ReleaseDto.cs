namespace PCMS.API.Dtos.Read
{
    /// <summary>
    /// DTO when you want to get a release
    /// </summary>
    public class ReleaseDto
    {
        public required string Id { get; set; }

        public required DateTime ReleaseDate { get; set; }

        public required string ReleaseType { get; set; }

        public required string Notes { get; set; }
    }
}