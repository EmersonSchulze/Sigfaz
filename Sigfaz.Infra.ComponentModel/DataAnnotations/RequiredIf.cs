using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class RequiredIf : ValidationAttribute
    {
        private readonly string _whenProperty;
        private readonly Type _ofType;
        private readonly string[] _validValues;

        public string WhenProperty
        {
            get { return _whenProperty; }
        }
        
        public string[] ValidValues
        {
            get { return _validValues; }
        }

        public RequiredIf(string whenProperty, Type ofType, string isValue)
        {
            if (string.IsNullOrWhiteSpace(whenProperty))
                throw new ArgumentNullException("whenProperty");

            if (ofType == null)
                throw new ArgumentNullException("whenProperty");

            if (!ofType.IsEnum)
                throw new ArgumentException("whenProperty deve ser um enum");

            if (string.IsNullOrWhiteSpace(isValue))
                throw new ArgumentNullException("isValue");

            this._whenProperty = whenProperty;
            this._ofType = ofType;
            this._validValues = isValue.Split(',');
        }

        protected override ValidationResult IsValid(object valor, ValidationContext validationContext)
        {
            // Como este atributo valida o preenchimento, caso esteja preenchido, retorna verdadeiro
            if (valor != null)
                return ValidationResult.Success;

            // Propriedade de referência
            var propriedadeReferencia = validationContext.ObjectType.GetProperty(_whenProperty);

            // Valor propriedade de referência. Caso seja nulo, retornar verdadeiro, pois independe o prenchimento da mesma
            var valorPropriedadeReferencia = propriedadeReferencia != null ? propriedadeReferencia.GetValue(validationContext.ObjectInstance, null) : null;
            if (valorPropriedadeReferencia == null)
                return ValidationResult.Success;

            // Bate o valor da propriedade de refência, com os valores configurados para a dependência
            // Caso algum caso dê verdadeiro, lançar um erro de preenchimento (lembre-se nesse ponto a propriedade atual sempre está nula). 
            foreach (var value in _validValues)
            {
                var valid = Enum.Parse(_ofType, value);
                if (valorPropriedadeReferencia.Equals(valid))
                {
                    var attrs = propriedadeReferencia.GetCustomAttributes(typeof(RequiredIf), false);
                    if (attrs.Any())
                        return ((RequiredIf)attrs[0]).IsValid(null, validationContext);
                    else
                        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            // Caso o valor da propriedade de referência não seja igual a um dos valores preenchidos, independente do preenchimento ou não
            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            if (String.IsNullOrEmpty(ErrorMessage))
                return "O campo " + name + " é obrigatório.";
            else
                return ErrorMessage;
        }
    }
}