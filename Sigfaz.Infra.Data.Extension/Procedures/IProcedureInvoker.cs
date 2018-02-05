using System;
using System.Collections.Generic;
using System.Data;

namespace Sigfaz.Infra.Data.Extension.Procedures
{
    public interface IProcedureInvoker
    {
        /// <summary>
        /// Executa procedure
        /// </summary>
        /// <param name="spName">Nome da procedure a ser executada</param>
        /// <param name="parameters">Lista do tipo OracleParameter com parâmetros</param>
        /// <returns></returns>
        void ExecuteProcedure(string spName, ref List<Parameter> parameters);

        /// <summary>
        /// Adiciona um novo parametro para a procedure
        /// </summary>
        /// <param name="nome">Nome da variável a ser adicionada</param>
        /// <param name="valor">Valor da variável</param>
        /// <param name="parameterDirection">Define o tipo de entrada da variável</param>
        /// <param name="type">Tipo da variável</param>
        /// <param name="tamanho">Define tamanho da variavel</param>
        /// <returns></returns>
        Parameter AddParameter(string nome, object valor, ParameterDirection parameterDirection, Type type, int tamanho = 0);
    }
}
