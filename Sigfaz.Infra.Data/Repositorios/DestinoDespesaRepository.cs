using System.Collections.Generic;
using System.Linq;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Repositorios;


namespace Sigfaz.Infra.Data.Repositorios
{
    public class DestinoDespesaRepository : RepositoryBase<DestinoDespesa>, IDestinoDespesaRepository
    {
        public IEnumerable<Cidade> BuscarPorEstado(string nome)
        {
            return Bd.Cidades.Where(c => c.Estado.Nome == nome);
        }

    }
}
