using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class ItemManutencaoAppService : AppServiceBase<ItemManutencao>, IItemManutencaoAppService
    {
        private readonly IItemManutencaoService itemApp;

        public ItemManutencaoAppService(IItemManutencaoService service) : base(service)
        {
            itemApp = service;
        }
    }
}
