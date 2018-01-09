using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigfaz.Infra.Data.Repositorios
{
    public class CidadeRepository : RepositoryBase<Cidade>, ICidadeRepository
    {

        public IEnumerable<Cidade> BuscarPorEstado(string nome)
        {
            return Bd.Cidades.Where(c => c.Estado.Nome == nome);
        }

    }
}
