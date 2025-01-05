using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorkTracker.Utils.Validators
{
    public class PasswordValidator: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = value as string;
            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult(false, "Password cannot be empty.");
            }

            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            if (!Regex.IsMatch(password, passwordPattern))
            {
                return new ValidationResult(false, "Password must be at least 8 characters long and include a letter, a number, and a special character.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
