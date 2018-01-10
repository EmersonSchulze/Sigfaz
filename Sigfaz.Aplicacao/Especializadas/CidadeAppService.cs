using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigfaz.Aplicacao.Especializadas
{
    public class CidadeAppService : AppServiceBase<Cidade>, ICidadeService
    {
        private readonly ICidadeService cidadeApp;

        public CidadeAppService(ICidadeService service) : base(service)
        {
            cidadeApp = service;
        }

        public IEnumerable<Cidade> BuscarPorEstado(string descricao)
        {
            return cidadeApp.BuscarPorEstado(descricao);
        }
    }
}
