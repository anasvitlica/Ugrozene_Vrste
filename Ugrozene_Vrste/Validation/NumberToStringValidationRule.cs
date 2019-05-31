using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ugrozene_Vrste.Validation
{
    class NumberToStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
               
                if (s.Contains("0") || s.Contains("1") || s.Contains("2") || s.Contains("3") ||
                    s.Contains("4") || s.Contains("5") || s.Contains("6") || s.Contains("7") ||
                    s.Contains("8") || s.Contains("9"))
                {
                    return new ValidationResult(false, "Nije dozvoljen unos cifara!");
                }
                else if (s.Equals(""))
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
