namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class BooleanExtensions
    {
        public static bool Is(this bool? bValue, bool bIs)
        {
            return bValue == bIs;
        }

        public static bool Is(this bool bValue, bool bIs)
        {
            return bValue == bIs;
        }
    }
}
