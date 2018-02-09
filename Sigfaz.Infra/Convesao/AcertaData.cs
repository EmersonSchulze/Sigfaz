using System;

namespace Sigfaz.Infra.Convesao
{
    public class AcertaData
    {
        /// <summary>
        /// Acerta a data com dia válido para o mês
        /// </summary>
        /// <param name="dia">Dia a ser utilizado</param>
        /// <param name="mes">Mês a ser utilizado</param>
        /// <param name="ano">Ano a ser utilizado</param>
        public static DateTime Acertar(int dia, int mes, int ano)
        {
            while (mes > 12)
            {
                mes -= 12;
                ano += 1;
            }
            while (mes <= 0)
            {
                mes += 12;
                ano -= 1;
            }

            if (dia > DiasPorMes(ano, mes))
                dia = DiasPorMes(ano, mes);
            else if (dia < 1)
                dia = 1;

            DateTime dataFaturamento = new DateTime(ano, mes, dia);

            return dataFaturamento;
        }

        /// <summary>
        /// Verifica o último dia de cada mês
        /// </summary>
        /// <param name="mes">Mês a ser utilizado</param>
        /// <param name="ano">Ano a ser utilizado</param>
        private static int DiasPorMes(Int32 ano, Int32 mes)
        {
            Int32[] daysInMonth = new Int32[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int retorno = daysInMonth[mes];
            if (mes == 2 && AnoBissexto(ano))
                retorno += 1;

            return retorno;
        }

        /// <summary>
        /// Verifica se o ano informado é bissexto
        /// </summary>
        /// <param name="ano">Ano a ser utilizado</param>
        private static bool AnoBissexto(int ano)
        {
            return (ano % 4 == 0 && (ano % 100 != 0 || ano % 400 == 0));
        }
    }
}
