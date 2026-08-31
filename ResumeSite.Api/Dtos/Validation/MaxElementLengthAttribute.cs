using System.ComponentModel.DataAnnotations;

namespace ResumeSite.Api.Dtos.Validation;

public class MaxElementLengthAttribute(int maxLength) : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value is not IEnumerable<string?> items)
    {
      return ValidationResult.Success;
    }

    var tooLong = items.Any(item => item is not null && item.Length > maxLength);

    return tooLong
      ? new ValidationResult($"Each item in {validationContext.MemberName} must be at most {maxLength} characters.")
      : ValidationResult.Success;
  }
}
