using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class TipoSanidadeService : ServiceBase<TipoSanidade>, ITipoSanidadeService
    {
        private readonly ITipoSanidadeRepository _repository;

        public TipoSanidadeService(ITipoSanidadeRepository tSanidadeRepository) : base(tSanidadeRepository)
        {
            _repository = tSanidadeRepository;
        }
    }
}
