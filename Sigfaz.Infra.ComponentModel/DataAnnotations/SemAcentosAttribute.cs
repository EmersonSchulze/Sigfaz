using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class SemAcentosAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = Convert.ToString(value);
            if (String.IsNullOrEmpty(text)) return true;
            // TODO. Atingamento possía dependência do RemoverAcentos. Criar uma regex para validar 
            //       isso
            return text.Equals(true);
        }

        public override string FormatErrorMessage(string name)
        {
            return "Este campo não aceita acentos.";
        }       
    }
}
