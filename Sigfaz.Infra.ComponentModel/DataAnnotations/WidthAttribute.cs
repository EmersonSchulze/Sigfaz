using System;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class WidthAttribute : Attribute
    {
        public Width Width { get; private set; }

        public WidthAttribute(Width width)
        {
            Width = width;
        }
    }
}
