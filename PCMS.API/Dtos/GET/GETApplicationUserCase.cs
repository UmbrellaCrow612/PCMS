namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO for GET a Application User Case
    /// </summary>
    public class GETApplicationUserCase
    {
        public required string UserId { get; set; }

        public required string CaseId { get; set; }
    }
}