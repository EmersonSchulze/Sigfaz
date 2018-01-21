using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class RacaService : ServiceBase<Raca>, IRacaService
    {
        private readonly IRacaRepository repository;

        public RacaService(IRacaRepository racaRepository) : base(racaRepository)
        {
            repository = racaRepository;
        }
    }
}
