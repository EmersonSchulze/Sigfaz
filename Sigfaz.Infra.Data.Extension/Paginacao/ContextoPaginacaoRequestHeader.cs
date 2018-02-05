namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    public class ContextoPaginacaoRequestHeader
    {
        public int PaginaAtual { get; set; }
        public int RegistrosPorPagina { get; set; }

        internal ContextoPaginacaoRequestHeader()
        { }

        public ContextoPaginacaoRequestHeader(int paginaAtual, int registrosPorPagina)
        {
            PaginaAtual = paginaAtual;
            RegistrosPorPagina = registrosPorPagina;
        }
    }
}
