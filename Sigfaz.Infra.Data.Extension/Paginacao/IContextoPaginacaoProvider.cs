using System;

namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    /// <summary>
    /// Responsável por armazenar a referência ao provider das informações de contexto de paginação
    /// </summary>
    public sealed class PaginacaoContextManager
    {
        /// <summary>
        /// Inicializa o manager 
        /// </summary>
        /// <param name="provider"></param>
        public static void Init(IContextoPaginacaoProvider provider)
        {
            if (PaginacaoContextManager._provider != null)
                throw new InvalidOperationException("PaginacaoContextManager já foi inicializado");

            PaginacaoContextManager._provider = provider;
        }

        private static IContextoPaginacaoProvider _provider;

        public static IContextoPaginacaoProvider CurrentProvider
        {
            get
            {
                if (PaginacaoContextManager._provider == null)
                    throw new InvalidOperationException("PaginacaoContextManager ainda não foi inicializado");

                return PaginacaoContextManager._provider;
            }
        }
    }
    
    public interface IContextoPaginacaoProvider
    {
        Paginacao.ContextoPaginacao CurrentContext { get; set; }
    }
}
