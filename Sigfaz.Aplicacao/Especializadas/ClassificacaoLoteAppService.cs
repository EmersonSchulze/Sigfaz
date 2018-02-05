using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class ClassificacaoLoteAppService : AppServiceBase<ClassificacaoLote>, IClassificacaoLoteAppService
    {
        private readonly IClassificacaoLoteService _classLoteApp;

        public ClassificacaoLoteAppService(IClassificacaoLoteService service) : base(service)
        {
            _classLoteApp = service;
        }
    }
}
