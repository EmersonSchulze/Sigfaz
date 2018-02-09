using System;

namespace Sigfaz.Infra.Convesao
{
    public class DataEmAnos
    {
        /// <summary>
        /// Cálculo da idade em anos.
        /// </summary>
        /// <param name="dataNascimento">Data de nascimento para cálculo com a data atual</param>
        public static int CalcularIdade(DateTime dataNascimento)
        {
            int anos = DateTime.Now.Year - dataNascimento.Year;

            if (DateTime.Now.Month < dataNascimento.Month || (DateTime.Now.Month == dataNascimento.Month && DateTime.Now.Day < dataNascimento.Day))
                anos--;
            return anos;

        }

        /// <summary>
        /// Cálculo da idade em anos.
        /// </summary>
        /// <param name="dataNascimento">Data de nascimento para cálculo com a data atual</param>
        /// <param name="dataBase">Data base para comparação</param>
        public static int CalcularIdade(DateTime dataNascimento, DateTime dataBase)
        {
            int anos = dataBase.Year - dataNascimento.Year;

            if (dataBase.Month < dataNascimento.Month || (dataBase.Month == dataNascimento.Month && dataBase.Day < dataNascimento.Day))
                anos--;
            return anos;

        }
    }
}
