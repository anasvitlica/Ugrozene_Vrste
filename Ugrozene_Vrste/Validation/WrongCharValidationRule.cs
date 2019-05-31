using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                
                if (s.Contains("?") || s.Contains(".") || s.Contains(",") || s.Contains("/") ||
                    s.Contains(">") || s.Contains("<") || s.Contains("|") || s.Contains("!") ||
                    s.Contains("@") || s.Contains("#") || s.Contains("$") || s.Contains("%") ||
                    s.Contains("^") || s.Contains("&") || s.Contains("*") || s.Contains("(") ||
                    s.Contains(")") || s.Contains("_") || s.Contains("-") || s.Contains("=") ||
                    s.Contains("+") || s.Contains("}") || s.Contains("{") || s.Contains("]") ||
                    s.Contains("[") || s.Contains("\\")|| s.Contains("`") || s.Contains("~"))
                {
                    return new ValidationResult(false, "Uneli ste nedozvoljen karakter!");
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
