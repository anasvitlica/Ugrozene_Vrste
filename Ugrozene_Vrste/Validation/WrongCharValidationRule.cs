using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Ugrozene_Vrste.Validation
{
    class WrongCharValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex r = new Regex("^[a-zA-Z0-9]+$");

                if (!r.IsMatch(s))
                {
                    return new ValidationResult(false, "Uneli ste nedozvoljen karakter!");
                }
                else if (s.Equals("") || s==null)
                {
                    return new ValidationResult(false, "Polje je obavezno!");
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Došlo je do greške!");
            }
        }
    }
}
