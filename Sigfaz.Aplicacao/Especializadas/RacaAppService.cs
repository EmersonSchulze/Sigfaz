using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class RacaAppService : AppServiceBase<Raca>, IRacaAppService
    {
        private readonly IRacaService _racaApp;

        public RacaAppService(IRacaService service) : base(service)
        {
            _racaApp = service;
        }
    }
}
