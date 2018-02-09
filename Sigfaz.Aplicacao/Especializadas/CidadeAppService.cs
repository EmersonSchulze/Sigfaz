using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;
using System.Collections.Generic;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
    {
        private readonly ICidadeService _cidadeApp;

        public CidadeAppService(ICidadeService service) : base(service)
        {
            _cidadeApp = service;
        }

        public IEnumerable<Cidade> BuscarPorEstado(string descricao)
        {
            return _cidadeApp.BuscarPorEstado(descricao);
        }
    }
}
