
using System.Collections.Generic;

namespace Sigfaz.Dominio.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Incluir(TEntity obj);

        TEntity BuscaId(int id);

        IEnumerable<TEntity> BuscaTodos();
        void Remover(TEntity obj);
        void Atualizar(TEntity obj);

        void Dispose();
    }
}
