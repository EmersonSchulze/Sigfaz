using System;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class Int64Extension
    {
        /// <summary>
        /// Retornar dígito Mod11, para valor com até 16 digitos
        /// </summary>
        /// <param name="_int64">Número para cálculo</param>
        public static Int64 Modulo11(this Int64 _int64)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };
            string strText = _int64.ToString();

            if (strText.Length > 16)
                throw new Exception("Número não suportado pela função!");

            int intSoma = 0;
            int intIdx = 0;
            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];
                intIdx++;
            }
            int intResto = (intSoma * 10) % 11;
            int intDigito = intResto;
            if (intDigito >= 10)
                intDigito = 0;

            return intDigito;
        }

        /// <summary>
        /// Retornar dígito Mod11 simples, para valor com até 16 digitos
        /// </summary>
        /// <param name="valor">Número para cálculo</param>
        public static Int64 Modulo11Simples(this Int64 _int64)
        {
            int mod = Convert.ToInt32(_int64) % 11;
            if (mod > 9)
                mod = 0;
            return mod;
        }
    }
}
