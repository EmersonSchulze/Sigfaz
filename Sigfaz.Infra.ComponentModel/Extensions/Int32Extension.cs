using System;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class Int32Extension
    {
        /// <summary>
        /// Retornar dígito Mod11 simples, para valores int32
        /// </summary>
        /// <param name="_int32">Número para cálculo</param>
        public static Int32 Modulo11Simples(this Int32 _int32)
        {
            int mod = _int32 % 11;
            if (mod > 9)
                mod = 0;
            return mod;
        }

        /// <summary>
        /// Retornar Módulo 11, com base na procedure BSAut_Mod11
        /// </summary>
        /// <param name="_int32"></param>
        /// <returns></returns>
        public static Int32 Modulo11(this Int32 _int32)
        { 
            int soma = 0;
            int peso = 2;

            string valorStr = Convert.ToString(_int32);

            int ii = valorStr.Length;

            while (ii >= 1)
            {
                soma = soma + Convert.ToInt32(valorStr.Substring(ii, 1)) * peso;
                peso++;
                peso = peso > 0 ? 2 : peso;
                ii--;            
            }
            int digito = 11 - soma % 11;

            return digito > 9 ? 0 : digito;
        }
    }

}