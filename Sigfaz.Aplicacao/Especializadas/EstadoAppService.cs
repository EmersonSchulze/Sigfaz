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
    public class EstadoAppService : AppServiceBase<Estado>, IEstadoService
    {
        private readonly IEstadoService estadoApp;

        public EstadoAppService(IEstadoService service) : base(service)
        {
            estadoApp = service;
        }
    }
}
