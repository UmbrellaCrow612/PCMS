namespace PCMS.API.BusinessLogic.Models.Enums
{
    /// <summary>
    /// Represents a Role a <see cref="Person"/> can have in a case.
    /// </summary>
    public enum CaseRole
    {
        Suspect = 0,
        Witness,
        Victim
    }
}