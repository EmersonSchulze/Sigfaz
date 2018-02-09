using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class GrupoAppService : AppServiceBase<Grupo>, IGrupoAppService
    {
        private readonly IGrupoService _grupoApp;

        public GrupoAppService(IGrupoService service) : base(service)
        {
            _grupoApp = service;
        }
    }
}
