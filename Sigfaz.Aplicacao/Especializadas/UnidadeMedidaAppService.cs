using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class UnidadeMedidaAppService : AppServiceBase<UnidadeMedida>, IUnidadeMedidaAppService
    {
        private readonly IUnidadeMedidaService _unidadeApp;

        public UnidadeMedidaAppService(IUnidadeMedidaService service) : base(service)
        {
            _unidadeApp = service;
        }
    }
}
