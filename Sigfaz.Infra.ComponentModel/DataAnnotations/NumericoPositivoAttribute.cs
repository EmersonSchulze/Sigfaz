using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class NumericoPositivoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var number = Convert.ToString(value);
            if (String.IsNullOrEmpty(number)) return true;
            Int64 i;
            Int64.TryParse(number, out i);

            return (i < 0) ? false : true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Este campo só aceita caracteres numéricos positivos.";
        }        
    }
}
