using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class SemCaractereEspecialAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = Convert.ToString(value);
            if (String.IsNullOrEmpty(text)) return true;
            text = text.ToUpperInvariant();
            var especial =
                text.Where(c => !((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')));
            return especial.Count() == 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Este campo não aceita caracteres especiais.";
        }
    }
}
