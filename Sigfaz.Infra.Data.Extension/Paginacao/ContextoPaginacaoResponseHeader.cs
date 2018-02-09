namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    public class ContextoPaginacaoResponseHeader
    {
        public long TotalRegistros { get; set; }

        internal ContextoPaginacaoResponseHeader()
        { }

        public ContextoPaginacaoResponseHeader(long totalRegistros)
        {
            TotalRegistros = totalRegistros;
        }

    }
}
