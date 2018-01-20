using AutoMapper;
using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.UnidadeMedida;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Estado
{
    public class UnidadeMedidasController : System.Web.Mvc.Controller
    {
        private readonly IUnidadeMedidaAppService appService;
        private readonly IMapper mapper;


        public UnidadeMedidasController(IUnidadeMedidaAppService repositorio)
        {
           this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Estado
        public ActionResult Index()
        {
            var unidadeViewModel = mapper.Map<IEnumerable<Dominio.Entidades.UnidadeMedida>, IEnumerable<UnidadeMedidaIndexViewModel>>(appService.BuscaTodos());
            return View(unidadeViewModel);
        }


    }
}
