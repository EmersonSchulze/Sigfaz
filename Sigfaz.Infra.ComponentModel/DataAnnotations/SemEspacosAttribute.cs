using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class SemEspacosAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = Convert.ToString(value);
            if (String.IsNullOrEmpty(text)) return true;
            return !text.Contains(" ");
        }

        public override string FormatErrorMessage(string name)
        {
            return "Este campo não aceita espaços.";
        }      
    }
}
