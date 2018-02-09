using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using MvcBreadCrumbs;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.UnidadeMedida;
using Sigfaz.Portal.AutoMapper;

namespace Sigfaz.Portal.Areas.Basico.Controllers.UnidadeMedida
{
    public class UnidadeMedidasController : System.Web.Mvc.Controller
    {
        private readonly IUnidadeMedidaAppService _appService;
        private readonly IMapper _mapper;


        public UnidadeMedidasController(IUnidadeMedidaAppService repositorio)
        {
           this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;
        }

        // GET: Estado
        public ActionResult Index()
        {
            BreadCrumb.Add(Url.Action("Index", "UnidadeMedidas", "UnidadeMedida_Default"), "Unidade de Medida");
            var unidadeViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.UnidadeMedida>, IEnumerable<UnidadeMedidaIndexViewModel>>(_appService.BuscaTodos());
            return View(unidadeViewModel);
        }


    }
}
