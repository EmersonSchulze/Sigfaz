using Sigfaz.Dominio.Entidades;
using System.Collections.Generic;

namespace Sigfaz.Aplicacao.Interfaces.Especializadas
{
    public interface ICidadeAppService : IAppServiceBase<Cidade>
    {
        IEnumerable<Cidade> BuscarPorEstado(string descricao);
    }
}
