using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ugrozene_Vrste.Validation
{
    class StringToDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                } else if (s.Equals(""))
                {
                    return new ValidationResult(false, "Polje je obavezno!");
                }
                return new ValidationResult(false, "Nije dozvoljen unos slova!");
            }
            catch
            {
                return new ValidationResult(false, "Došlo je do greške!");
            }
        }
    }
}
