using System.Globalization;
using System.Windows.Controls;

namespace Assignment.UI;
public class MaxLengthRule : ValidationRule
{
    public int MaxLength { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value != null && value.ToString().Length > MaxLength)
        {
            return new ValidationResult(false, $"Text cannot exceed {MaxLength} characters");
        }

        return ValidationResult.ValidResult;
    }
}
