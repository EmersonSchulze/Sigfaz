using System;

namespace Sigfaz.Infra.Mvc.Attributes
{
    public sealed class DvAttribute : Attribute
    {
        public readonly int Digits;

        public DvAttribute(int digits = 5)
        {
            Digits = digits;
        }
    }
}
