using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class GrupoAppService : AppServiceBase<Grupo>, IGrupoAppService
    {
        private readonly IGrupoService grupoApp;

        public GrupoAppService(IGrupoService service) : base(service)
        {
            grupoApp = service;
        }
    }
}
