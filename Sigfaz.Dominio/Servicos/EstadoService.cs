using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;

namespace Sigfaz.Dominio.Servicos
{
    public class EstadoService : ServiceBase<Estado>, IEstadoService
    {
        private readonly IEstadoRepository _repository;

        public EstadoService(IEstadoRepository estadoRepository) : base(estadoRepository)
        {
            _repository = estadoRepository;
        }
    }
}
