using Sigfaz.Dominio.Entidades;
using System.Collections.Generic;

namespace Sigfaz.Dominio.Interfaces.Servicos
{
    public interface ICidadeService : IServiceBase<Cidade>
    {
        IEnumerable<Cidade> BuscarPorEstado(string descricao);
    }
}
