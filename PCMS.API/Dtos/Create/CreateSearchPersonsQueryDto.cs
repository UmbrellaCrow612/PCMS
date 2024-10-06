namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// List of paramas sent across when searching foir a person, which will be used to filter for people based on the params.
    /// </summary>
    /// <remarks>
    /// When sending these search paramas keep fields that have no value as null or you are not trying to search with becuase if its empty string it will try to match those.
    /// </remarks>
    public class CreateSearchPersonsQueryDto
    {
        public string? FullName { get; set; }

        public string? ContactInfo { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
