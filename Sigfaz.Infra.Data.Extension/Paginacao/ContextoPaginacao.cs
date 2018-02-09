using System;

namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    public class ContextoPaginacao : IDisposable
    {
        public const String HeaderName = "ContextoPaginacao";
        
        public const String HeaderNamespace = "http://www.Sigfaz.com.br/infra/services";

        public readonly ContextoPaginacaoRequestHeader HeaderRequest;
        
        public ContextoPaginacaoResponseHeader HeaderResponse { get; internal set; }

        internal ContextoPaginacao(int paginaAtual, int registrosPorPagina)
        {
            HeaderRequest = new ContextoPaginacaoRequestHeader(paginaAtual, registrosPorPagina);
            HeaderResponse = new ContextoPaginacaoResponseHeader();
        }

        public static ContextoPaginacao Current
        {
            get
            {
                try
                {
                    return PaginacaoContextManager.CurrentProvider.CurrentContext;
                }
                catch
                {
                    return null;
                }
            }
        }

        public int PaginaAtual
        {
            get
            {
                return (HeaderRequest != null) ? HeaderRequest.PaginaAtual : 0;
            }
        }

        public int RegistrosPorPagina
        {
            get
            {
                return (HeaderRequest != null) ? HeaderRequest.RegistrosPorPagina : 0;
            }
        }

        public long TotalRegistros
        {
            get
            {
                return (HeaderResponse != null) ? HeaderResponse.TotalRegistros : 0;
            }
            set
            {
                HeaderResponse.TotalRegistros = value;
                //ContextoPaginacaoHelper.AdicionarHeaderContextoPaginacaoParaResposta(value); TODO
            }
        }

        public void Dispose()
        {
            try
            {
                lock (ContextoPaginacaoHelper.ContextosConsulta)
                {
                    ContextoPaginacaoHelper.ContextosConsulta.Remove(this);
                }
            }
            catch
            {
            }
        }
    }
}
