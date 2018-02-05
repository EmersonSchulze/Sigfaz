using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sigfaz.Infra.Mvc.Attributes
{
    public class CnpjAttribute : CnpjCpfAttribute, IClientValidatable
    {
        private static readonly string[] Blacklist = {
            "11111111111111",
            "22222222222222",
            "33333333333333",
            "44444444444444",
            "55555555555555",
            "66666666666666",
            "77777777777777",
            "88888888888888",
            "99999999999999"
        };

        public override bool IsValid(object value)
        {
            try
            {
                var text = Convert.ToString(value);
                return String.IsNullOrEmpty(text) || ValidaCnpj(text);
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
                ErrorMessage = FormatErrorMessage(null),
                ValidationType = "cnpjvalidation"
            };
        }

        public override string FormatErrorMessage(string name)
        {
            return "O CNPJ informado é inválido.";
        }

        internal static bool ValidaCnpj(string cnpj)
        {
            var stripped = Strip(cnpj);
            if (stripped.Length != 14)
                return false;

            if (Blacklist.Contains(stripped))
                return false;

            var numbers = stripped.Substring(0, 12);
            numbers += VerifierDigit(numbers);
            numbers += VerifierDigit(numbers);


            return numbers.Substring(numbers.Length - 2, 2) == stripped.Substring(stripped.Length - 2, 2);
        }

        private static int VerifierDigit(string numbers)
        {
            var index = 2;
            var numberEnum = numbers.Select(digit => int.Parse(digit.ToString())).Reverse();

            var sum = numberEnum.Aggregate(0, (buffer, number) =>
            {
                buffer += number*index;
                index = (index == 9) ? 2 : index + 1;
                return buffer;
            });
            var mod = sum % 11;
            return (mod < 2 ? 0 : 11 - mod);
        }
    }
}
