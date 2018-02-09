using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = Convert.ToString(value).Trim();
            if (String.IsNullOrEmpty(text)) return true;
            //return Regex.IsMatch(text, @"\w+@[a-zA-Z0-9_]+?\.[a-zA-Z0-9]{2,3}");
            return Regex.IsMatch(text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

        public override string FormatErrorMessage(string name)
        {
            return "O e-mail informado não é válido.";
        }
    }
}
