using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class UnidadeMedidaAppService : AppServiceBase<UnidadeMedida>, IUnidadeMedidaAppService
    {
        private readonly IUnidadeMedidaService unidadeApp;

        public UnidadeMedidaAppService(IUnidadeMedidaService service) : base(service)
        {
            unidadeApp = service;
        }
    }
}
