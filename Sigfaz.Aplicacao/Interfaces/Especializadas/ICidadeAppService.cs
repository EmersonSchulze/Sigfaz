using System.Collections.Generic;
using Sigfaz.Dominio.Entidades;

namespace Sigfaz.Aplicacao.Interfaces.Especializadas
{
    public interface ICidadeAppService : IAppServiceBase<Cidade>
    {
        IEnumerable<Cidade> BuscarPorEstado(string descricao);
    }
}
