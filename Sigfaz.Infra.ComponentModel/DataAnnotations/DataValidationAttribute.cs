using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{

    public sealed class IsVigenciaValida : ValidationAttribute
    {
        private readonly string _propriedadeTestada;
        private readonly bool _allowEquals;

        public IsVigenciaValida(string propriedadeTestada, bool allowEquals = false)
        {
            this._propriedadeTestada = propriedadeTestada;
            this._allowEquals = allowEquals;
        }

        protected override ValidationResult IsValid(object dataReferencia, ValidationContext validationContext)
        {
            var propriedadesData = validationContext.ObjectType.GetProperty(_propriedadeTestada);
            if (propriedadesData == null)
            {
                return new ValidationResult(string.Format("Propriedade Desconhecida {0}", _propriedadeTestada));
            }

            var valorData = propriedadesData.GetValue(validationContext.ObjectInstance, null);

            if (dataReferencia == null || !(dataReferencia is DateTime))
                return ValidationResult.Success;

            if (valorData == null || !(valorData is DateTime))
                return ValidationResult.Success;

            if ((DateTime) dataReferencia >= (DateTime) valorData)
            {
                // Se permitir igualdade, retorna validação com sucesso.
                if (_allowEquals)
                    return ValidationResult.Success;

                if ((DateTime) dataReferencia > (DateTime) valorData)
                    return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}



