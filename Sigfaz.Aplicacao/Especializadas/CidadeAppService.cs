using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;
using System.Collections.Generic;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
    {
        private readonly ICidadeService cidadeApp;

        public CidadeAppService(ICidadeService service) : base(service)
        {
            cidadeApp = service;
        }

        public IEnumerable<Cidade> BuscarPorEstado(string descricao)
        {
            return cidadeApp.BuscarPorEstado(descricao);
        }
    }
}
