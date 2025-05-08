using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Attributes;
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value , ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        if (value is DateOnly date)
        {
            if (date >= DateOnly.FromDateTime(DateTime.Now))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Date must be in the future");
        }
        return new ValidationResult("Invalid date format");
    }
}