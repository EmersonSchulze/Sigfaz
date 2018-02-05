using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sigfaz.Infra.Mvc.Attributes
{
    public abstract class CnpjCpfAttribute : ValidationAttribute
    {
        protected static string Strip(string text)
        {
            var reg = new Regex(@"[^0-9]");
            return reg.Replace(text, string.Empty);
        }
    }
}
