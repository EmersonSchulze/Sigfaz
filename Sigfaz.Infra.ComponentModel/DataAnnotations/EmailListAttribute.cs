using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class EmailListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = Convert.ToString(value);
           
            string[] emails = null;

            if (text.Split(';').Length > 0)
                emails = text.Split(';');
            else if (text.Split(',').Length > 0)
                emails = text.Split(',');
            else
                emails = new string[] { text };

            var validador = new EmailAttribute();

            foreach (var item in emails)
                if (!validador.IsValid(item.Trim()))
                    return false;

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Os e-mails informados não são válidos.";
        }
    }
}
