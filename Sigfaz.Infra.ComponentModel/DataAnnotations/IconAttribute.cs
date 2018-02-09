using System;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class IconAttribute : Attribute
    {
        public string CssClass { get; private set; }

        public IconAttribute(string cssClass)
        {
            CssClass = cssClass;
        }
    }
}