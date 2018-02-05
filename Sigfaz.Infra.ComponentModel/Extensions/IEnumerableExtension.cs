using System;
using System.Collections.Generic;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class IEnumerableExtentions
    {
        /// <summary>
        /// Aplica uma ação sobre todos os itens de uma enumeração
        /// </summary>
        /// <typeparam name="T">Tipo da enumeração</typeparam>
        /// <param name="enumerable">Enumeração sobre a qual será aplicada a ação</param>
        /// <param name="action">Ação a ser aplicada</param>
        /// <returns>Enumeração passada por parâmetro</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);

            return enumerable;
        }

        /// <summary>
        /// Retorna a enumeração atual exceto o último item. ATENÇÃO: Não indicado para utilizar em conjunto com o NHibernate,
        /// pois carrega todos os itens da enumeração para a memória.
        /// </summary>
        /// <typeparam name="T">Tipo da enumeração</typeparam>
        /// <param name="enumerable">Enumeração que será aplicada</param>
        /// <returns>Enumeração atual exceto o último item</returns>
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> enumerable)
        {
            return SkipLast(enumerable, 1);
        }

        /// <summary>
        /// Retorna a enumeração atual exceto os últimos N itens. ATENÇÃO: Não indicado para utilizar em conjunto com o NHibernate,
        /// pois carrega todos os itens da enumeração para a memória.
        /// </summary>
        /// <typeparam name="T">Tipo da enumeração</typeparam>
        /// <param name="enumerable">Enumeração que será aplicada</param>
        /// <param name="n">Quantidade de itens a ser removidos do final da enumeração</param>
        /// <returns>Enumeração atual exceto o último item</returns>
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> enumerable, int n)
        {
            var it = enumerable.GetEnumerator();
            bool hasRemainingItems = false;
            var cache = new Queue<T>(n + 1);

            do
            {
                if (hasRemainingItems = it.MoveNext())
                {
                    cache.Enqueue(it.Current);
                    if (cache.Count > n)
                        yield return cache.Dequeue();
                }
            } while (hasRemainingItems);
        }
    }
}
