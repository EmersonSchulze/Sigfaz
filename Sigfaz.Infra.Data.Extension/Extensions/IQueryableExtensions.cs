using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sigfaz.Infra.ComponentModel;

namespace Sigfaz.Infra.Data.Extension.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filtra os elementos da query, de forma que permaneçam apenas os que possuem o valor selecionado na coleção.
        /// </summary>
        /// <param name="query">A query sobre a qual será aplicado o filtro</param>
        /// <param name="selecionador">Expressão a ser utilizada para extrair o valor de um elemento da query</param>
        /// <param name="colecao">Valores que poderão ser obtidos pelo selecionador para que o elemento permaneça na query</param>
        /// <returns>Query cuja aplicação do selecionador em qualquer elemento, obtém-se um valor da coleção</returns>
        public static IQueryable<TElement> In<TElement, TValue>(this IQueryable<TElement> query, Expression<Func<TElement, TValue>> selecionador, ICollection<TValue> colecao)
        {
            if (null == selecionador) { throw new ArgumentNullException("valueSelector"); }
            if (null == colecao) { throw new ArgumentNullException("values"); }
            ParameterExpression p = selecionador.Parameters.Single();
            if (!colecao.Any())
            {
                return Enumerable.Empty<TElement>().AsQueryable();
            }

            var equals = colecao.Select(value => (Expression)Expression.Equal(selecionador.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.OrElse(accumulate, equal));
            return query.Where(Expression.Lambda<Func<TElement, bool>>(body, p));
        }

        public static IQueryable<TElement> ExcludeSelf<TElement>(this IQueryable<TElement> query, long handle) where TElement : Entidade
        {
            return query.Where(m => m.Handle != handle);
        }
    }

}
