using System.Collections.Generic;
using System.Linq;

namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    public static class ContextoPaginacaoExtensions
    {
        public static IEnumerable<T> Paginar<T>(this IEnumerable<T> source, Paginacao.ContextoPaginacao contexto)
        {
            if (contexto == null)
                return source;
            contexto.TotalRegistros = source.Count();
            return source
                .Skip((contexto.PaginaAtual - 1) * contexto.RegistrosPorPagina)
                .Take(contexto.RegistrosPorPagina);
        }

        public static IQueryable<T> Paginar<T>(this IQueryable<T> source, Paginacao.ContextoPaginacao contexto)
        {
            if (contexto == null)
                return source;
            contexto.TotalRegistros = source.Count();
            return source
                .Skip((contexto.PaginaAtual - 1) * contexto.RegistrosPorPagina)
                .Take(contexto.RegistrosPorPagina);
        }
    }
}
