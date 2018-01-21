using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class CulturaAppService : AppServiceBase<Cultura>, ICulturaAppService
    {
        private readonly ICulturaService culturaApp;

        public CulturaAppService(ICulturaService service) : base(service)
        {
            culturaApp = service;
        }
    }
}
