using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sigfaz.Infra.Data.Extension.Repositorios
{
    public interface IRepositorioLeitura<T> 
    {
        /// <summary>
        /// Retorna a Entidade através do handle informado
        /// </summary>
        /// <param name="handle">Handle identificador da entidade</param>
        /// <returns>Entidade localizada. Nulo caso não encontrada.</returns>
        T Retornar(long handle);

        /// <summary>
        /// Retorna todas as Entidades do tipo
        /// </summary>
        /// <returns>Queriable que permite percorrer todas as instâncias ou filtrar o resultado ainda mais.</returns>
        IQueryable<T> RetornarTodos();

        /// <summary>
        /// Consulta Todas as Entidades de acordo com condição where
        /// </summary>
        /// <param name="where">Predicado para filtragem</param>
        /// <returns>Queriable que permite percorrer todas as instâncias ou filtrar o resultado ainda mais.</returns>
        IQueryable<T> Consultar(Expression<Func<T, bool>> where = null);
    }
}
