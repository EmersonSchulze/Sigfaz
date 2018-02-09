using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class UnidadeMedidaService : ServiceBase<UnidadeMedida>, IUnidadeMedidaService
    {
        private readonly IUnidadeMedidaRepository _repository;

        public UnidadeMedidaService(IUnidadeMedidaRepository unidadeRepository) : base(unidadeRepository)
        {
            _repository = unidadeRepository;
        }
    }
}
