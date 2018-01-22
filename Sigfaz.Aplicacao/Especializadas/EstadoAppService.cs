using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class EstadoAppService : AppServiceBase<Estado>, IEstadoAppService
    {
        private readonly IEstadoService estadoApp;

        public EstadoAppService(IEstadoService service) : base(service)
        {
            estadoApp = service;
        }
    }
}
