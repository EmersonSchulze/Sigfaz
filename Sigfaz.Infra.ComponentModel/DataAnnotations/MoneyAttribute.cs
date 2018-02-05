using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class MoneyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var s = value as string;
            if (s == null) return true;
            try
            {
                decimal.Parse(s, System.Globalization.NumberStyles.Currency);
                return true;
            } catch(Exception)
            {
                decimal d;
                return decimal.TryParse(s, out d);
            }
        }
    }
}
