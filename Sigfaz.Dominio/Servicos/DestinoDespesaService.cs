using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class DestinoDespesaService : ServiceBase<DestinoDespesa>, IDestinoDespesaService
    {
        private readonly IDestinoDespesaRepository repository;

        public DestinoDespesaService(IDestinoDespesaRepository destinoRepository) : base(destinoRepository)
        {
            repository = destinoRepository;
        }
    }
}
