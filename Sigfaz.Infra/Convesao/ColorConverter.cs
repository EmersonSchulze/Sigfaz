namespace Sigfaz.Infra.Convesao
{
    public static class ColorConverter
    {

        /// <summary>
        /// Gera um número long correspondente ao RGB passado.
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <returns></returns>
        public static long RgbToLong(long r, long g, long b)
        {
            var colorCode = (65536 * b) + (256 * g) + (r);

            return colorCode;
        }

        /// <summary>
        /// Retorna o RGB de um long
        /// </summary>
        /// <param name="colorCode">Código da cor em </param>
        /// <returns>Um array de 3 posições, onde: 0 - Red / 1 - Green / 2 - Blue</returns>
        public static long[] LongToRgb(long colorCode)
        {
            var blue  = colorCode / 65536;
            var green = (colorCode - (blue * 65536)) / 256;
            var red   = colorCode - (blue * 65536) - (green * 256);

            return new long[] { red, green, blue };
        }
    }
}
