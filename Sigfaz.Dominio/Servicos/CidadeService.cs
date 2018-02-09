using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;
using System.Collections.Generic;

namespace Sigfaz.Dominio.Servicos
{
    public class CidadeService : ServiceBase<Cidade>, ICidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository cidadeRepository) : base(cidadeRepository)
        {
            _repository = cidadeRepository;
        }

        public IEnumerable<Cidade> BuscarPorEstado(string descricao)
        {
            return _repository.BuscarPorEstado(descricao);
        }
    }
}
