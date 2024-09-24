namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a ApplicationUserCase
    /// </summary>
    public class GETApplicationUserCase
    {
        public required string UserId { get; set; }

        public required string CaseId { get; set; }
    }
}