using System;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class DoubleExtension
    {
        public static string FormatarValor(this double valor, int casasDecimais = 2)
        {
            double? valorNullable = valor;
            return valorNullable.FormatarValor(casasDecimais, false);
        }

        public static string FormatarValor(this double? valor, int casasDecimais = 2, bool zeroSeNulo = true)
        {
            if (zeroSeNulo)
                valor = valor ?? 0;
            var zeros = (casasDecimais == 0) ? String.Empty : ".";
            for (var i = 1; i <= casasDecimais; i++)
                zeros += "0";

            return String.Format("{0:0" + zeros + "}", valor);
        }
    }
}
