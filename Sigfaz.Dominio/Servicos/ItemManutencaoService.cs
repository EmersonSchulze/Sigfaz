using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class ItemManutencaoService : ServiceBase<ItemManutencao>, IItemManutencaoService
    {
        private readonly IItemManutencaoRepository repository;

        public ItemManutencaoService(IItemManutencaoRepository itemRepository) : base(itemRepository)
        {
            repository = itemRepository;
        }
    }
}
