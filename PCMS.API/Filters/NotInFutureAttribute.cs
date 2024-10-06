using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Filters
{
    /// <summary>
    /// Custom Data Annotation to make sure a Date Time field is not in the future.
    /// </summary>
    public class NotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                return true;
            }

            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }

            return true;
        }
    }
}