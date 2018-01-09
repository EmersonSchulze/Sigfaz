using Sigfaz.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigfaz.Dominio.Interfaces
{
    public interface ICidadeRepository : IRepositoryBase<Cidade>
    {
        IEnumerable<Cidade> BuscarPorEstado(string descricao);
    }
}
