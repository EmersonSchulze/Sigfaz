using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class GrupoService : ServiceBase<Grupo>, IGrupoService
    {
        private readonly IGrupoRepository _repository;

        public GrupoService(IGrupoRepository grupoRepository) : base(grupoRepository)
        {
            _repository = grupoRepository;
        }
    }
}
