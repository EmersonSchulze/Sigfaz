using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class CulturaAppService : AppServiceBase<Cultura>, ICulturaAppService
    {
        private readonly ICulturaService _culturaApp;

        public CulturaAppService(ICulturaService service) : base(service)
        {
            _culturaApp = service;
        }
    }
}
