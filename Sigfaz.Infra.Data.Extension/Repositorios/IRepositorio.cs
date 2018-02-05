using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sigfaz.Infra.Data.Extension.Repositorios
{
    public interface IRepositorio<T> : IRepositorioLeitura<T>
    {
        T Incluir(T entity);

        void IncluirVarios(IEnumerable<T> entity);
       
        T Alterar(T entity);

        void Recarregar(T entity);
        
        T Salvar(T entity);

        IQueryable<T> SalvarVarios(IQueryable<T> listEntity);

        void Excluir(T entity);

        void ExcluirVarios(IEnumerable<T> entity);

        void ExcluirVarios(Expression<Func<T, bool>> where);

        void EvitarAlteracoes(T entity);

        // Retorna estado original da Entidade (sem alterações da sessão/transação atual) através do handle informado
        T RetornarEstadoOriginal(long? handle);

        // Retorna o nome da tabela mapeada
        string NomeTabela { get; }
    }
}
