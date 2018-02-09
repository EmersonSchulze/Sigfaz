using System;
using System.Collections.Generic;

namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    public class ContextoPaginacaoHelper
    {
        internal static List<Paginacao.ContextoPaginacao> ContextosConsulta = new List<Paginacao.ContextoPaginacao>();        

        public static Paginacao.ContextoPaginacao NovoContextoPaginacao(int paginaAtual, int registrosPorPagina)
        {
            if (paginaAtual <= 0)
                throw new ArgumentOutOfRangeException("Página atual deve ser maior que zero!");
            
            if (registrosPorPagina <= 0)
                throw new ArgumentOutOfRangeException("Registros por página deve ser maior que zero!");

            var contextoPaginacao = new Paginacao.ContextoPaginacao(paginaAtual, registrosPorPagina);

            PaginacaoContextManager.CurrentProvider.CurrentContext = contextoPaginacao;

            return contextoPaginacao;
        }
    }
}
