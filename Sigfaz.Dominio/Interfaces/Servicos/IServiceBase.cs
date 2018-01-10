using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigfaz.Dominio.Interfaces.Servicos
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        void Incluir(TEntity obj);

        TEntity BuscaId(int id);

        IEnumerable<TEntity> BuscaTodos();
        void Remover(TEntity obj);
        void Atualizar(TEntity obj);

        void Dispose();
    }
}
