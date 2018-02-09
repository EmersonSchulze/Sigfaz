using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class PorcentagemAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var s = value as string;
            if (s == null) return true;
            try
            {
                double.Parse(s, System.Globalization.NumberStyles.Currency);
                return true;
            }
            catch (Exception)
            {
                double d;
                return double.TryParse(s, out d);
            }
        }        
    }
}
