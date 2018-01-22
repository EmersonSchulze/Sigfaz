using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class RacaAppService : AppServiceBase<Raca>, IRacaAppService
    {
        private readonly IRacaService racaApp;

        public RacaAppService(IRacaService service) : base(service)
        {
            racaApp = service;
        }
    }
}
