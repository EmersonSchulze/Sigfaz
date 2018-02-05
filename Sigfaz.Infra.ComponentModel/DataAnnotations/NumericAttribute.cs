using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class NumericAttribute : ValidationAttribute
    {
        private readonly int _integerDigits;
        public int DecimalDigits { get { return _decimalDigits; } }

        private readonly int _decimalDigits;
        public int IntegerDigits { get { return _integerDigits; } }

        public NumericAttribute(int integerDigits = 9, int decimalDigits = 0)
        {
            this._integerDigits = integerDigits;
            this._decimalDigits = decimalDigits;
        }

        public override bool IsValid(object value)
        {
            var number = Convert.ToString(value);
            if (String.IsNullOrEmpty(number)) return true;

            if (DecimalDigits == 0)
                return Regex.IsMatch(number, @"^\d{0," + IntegerDigits + "}$");

            return Regex.IsMatch(number, @"^\d{0," + IntegerDigits + @"}.\d{0," + DecimalDigits + "}$");
        }

        public override string FormatErrorMessage(string name)
        {
            return "Este campo só aceita caracteres numéricos.";
        }        
    }
}
