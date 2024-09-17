using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Filters
{
    /// <summary>
    /// Custom Attribute to make sure a provided enum value is a valid one defined in that enum.
    /// </summary>
    public class ValidEnumValueAttribute(Type enumType) : ValidationAttribute
    {
        private readonly Type _enumType = enumType;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Enum.IsDefined(_enumType, value))
            {
                return new ValidationResult($"Invalid value '{value}' for enum '{_enumType.Name}'.");
            }

            return ValidationResult.Success;
        }
    }
}
