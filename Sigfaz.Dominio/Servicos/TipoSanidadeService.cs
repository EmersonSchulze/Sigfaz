using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class TipoSanidadeService : ServiceBase<TipoSanidade>, ITipoSanidadeService
    {
        private readonly ITipoSanidadeRepository repository;

        public TipoSanidadeService(ITipoSanidadeRepository tSanidadeRepository) : base(tSanidadeRepository)
        {
            repository = tSanidadeRepository;
        }
    }
}
