using Sigfaz.Dominio.Entidades;
using System.Collections.Generic;

namespace Sigfaz.Dominio.Interfaces.Repositorios
{
    public interface ICidadeRepository : IRepositoryBase<Cidade>
    {
        IEnumerable<Cidade> BuscarPorEstado(string descricao);
    }
}
