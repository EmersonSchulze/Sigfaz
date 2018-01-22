using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class CulturaService : ServiceBase<Cultura>, ICulturaService
    {
        private readonly ICulturaRepository repository;

        public CulturaService(ICulturaRepository culturaRepository) : base(culturaRepository)
        {
            repository = culturaRepository;
        }
    }
}
