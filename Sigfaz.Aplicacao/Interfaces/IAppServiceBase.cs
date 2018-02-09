using System.Collections;
using System.Collections.Generic;
using Sigfaz.Dominio.Entidades;

namespace Sigfaz.Aplicacao.Interfaces
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        void Incluir(TEntity obj);
        TEntity BuscaId(int id);
        IEnumerable<TEntity> BuscaTodos();

        IEnumerable BuscaPrimeiros(int qtd);
        void Remover(TEntity obj);
        void Atualizar(TEntity obj);
        void Dispose();
    }
}
