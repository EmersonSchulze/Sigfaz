using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class DestinoDespesaAppService : AppServiceBase<DestinoDespesa>, IDestinoDespesaAppService
    {
        private readonly IDestinoDespesaService destinoApp;

        public DestinoDespesaAppService(IDestinoDespesaService service) : base(service)
        {
            destinoApp = service;
        }
    }
}
