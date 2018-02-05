using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sigfaz.Infra.Mvc.Attributes
{
    public class CpfAttribute : CnpjCpfAttribute, IClientValidatable
    {
        private static readonly string[] Blacklist = {
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999",
            "12345678909"
        };

        public override bool IsValid(object value)
        {
            try
            {
                var text = Convert.ToString(value);
                return string.IsNullOrEmpty(text) || IsValidCpf(text);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "cpfvalidation"
            };
        }

        public override string FormatErrorMessage(string name)
        {
            return "O CPF informado é inválido.";
        }

        internal static bool IsValidCpf(string cpf)
        {
            var stripped = Strip(cpf);
            if (stripped.Length != 11)
                return false;

            if (Blacklist.Contains(stripped))
                return false;

            var numbers = stripped.Substring(0, 9);
            numbers += VerifierDigit(numbers);
            numbers += VerifierDigit(numbers);

            return numbers.Substring(numbers.Length - 2, 2) == stripped.Substring(stripped.Length - 2, 2);
        }

        private static int VerifierDigit(string numbers)
        {
            var numberEnum = numbers.Select(digit => int.Parse(digit.ToString()));
            var modulus = numberEnum.Count() + 1;
            var multiplied = numberEnum.Select((m, index) => m * (modulus - index));
            var mod = multiplied.Aggregate((buffer, number) => buffer + number) % 11;
            return (mod < 2 ? 0 : 11 - mod);
        }
    }
}
