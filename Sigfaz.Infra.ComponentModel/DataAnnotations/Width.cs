using System;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public enum Width
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Six = 6,
        Twelve = 12
    }

    public static class WidthExtensions
    {
        public static string GetNameCssClass(this Width width)
        {
            return String.Format("size-{0:D}", Enum.Parse(typeof(Width), width.ToString()));
        }
    }
}
